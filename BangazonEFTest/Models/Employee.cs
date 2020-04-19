using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BangazonEFTest.Models
{
    public class Employee
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        [Required]
        public int ComputerId { get; set; }
        public Computer Computer { get; set; }
    }
}
