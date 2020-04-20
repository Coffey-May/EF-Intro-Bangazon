using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangazonEFTest.Models
{
    public class EmployeeCreateViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int DepartmentId { get; set; }
        public int ComputerId { get; set; }
        public List<SelectListItem> DepartmentOptions { get; set; }
        public List<SelectListItem> ComputerOptions { get; set; }
    }
}
