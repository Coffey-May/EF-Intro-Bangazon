using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BangazonEFTest.Data;
using BangazonEFTest.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BangazonEFTest.Controllers
{
    public class EmployeesController : Controller
    {
        // Our controllers have a reference to our database via the _context field
        private readonly ApplicationDbContext _context;

        public EmployeesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Employee
        public ActionResult Index()
        {
            // This queries the Employee table. Using the `Include` method will join the related entities
            var employees = _context.Employee
                .Include(e => e.Computer)
                .Include(e => e.Department)
                .ToList();

            return View(employees);
        }

        // GET: Employee/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Employee/Create
        public async Task<ActionResult> Create()
        {
            // Entity Framework will always return data from the database in the form of data models.
            // If data models are not the type of object you want for your view, you can use the `Select` method
            // EXAMPLE: We don't want a list of Computer and Department Objects here. We want a list of
            // SelectListItems.
            var allComputers = await _context.Computer
                .Where(c => c.DecomissionDate == null && c.Employee == null)
                .Select(d => new SelectListItem() { Text = d.Model, Value = d.Id.ToString() })
                .ToListAsync();
            var allDepartments = await _context.Department
                .Select(d => new SelectListItem() { Text = d.Name, Value = d.Id.ToString() })
                .ToListAsync();

            var viewModel = new EmployeeCreateViewModel();

            viewModel.DepartmentOptions = allDepartments;
            viewModel.ComputerOptions = allComputers;

            return View(viewModel);
        }

        // POST: Employee/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(EmployeeCreateViewModel employeeViewModel)
        {
            try
            {
                var employee = new Employee
                {
                    FirstName = employeeViewModel.FirstName,
                    LastName = employeeViewModel.LastName,
                    Email = employeeViewModel.Email,
                    DepartmentId = employeeViewModel.DepartmentId,
                    ComputerId = employeeViewModel.ComputerId,
                };

                _context.Employee.Add(employee);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Employee/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var allComputers = await _context.Computer
                .Select(d => new SelectListItem() { Text = d.Model, Value = d.Id.ToString() })
                .ToListAsync();
            var allDepartments = await _context.Department
                .Select(d => new SelectListItem() { Text = d.Name, Value = d.Id.ToString() })
                .ToListAsync();

            var employee = _context.Employee.FirstOrDefault(e => e.Id == id);

            var viewModel = new EmployeeCreateViewModel()
            {
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Email = employee.Email,
                ComputerId = employee.ComputerId,
                DepartmentId = employee.DepartmentId,
                ComputerOptions = allComputers,
                DepartmentOptions = allDepartments
            };
            
            return View(viewModel);
        }

        // POST: Employee/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, EmployeeCreateViewModel employeeViewModel)
        {
            try
            {
                // TODO: Add update logic here
                var employee = new Employee()
                {
                    Id = id,
                    FirstName = employeeViewModel.FirstName,
                    LastName = employeeViewModel.LastName,
                    Email = employeeViewModel.Email,
                    ComputerId = employeeViewModel.ComputerId,
                    DepartmentId = employeeViewModel.DepartmentId,
                };

                _context.Employee.Update(employee);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Employee/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var employee = await _context.Employee.FirstOrDefaultAsync(e => e.Id == id);

            return View(employee);
        }

        // POST: Employee/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, Employee employee)
        {
            try
            {
                _context.Employee.Remove(employee);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}