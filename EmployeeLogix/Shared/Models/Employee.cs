using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeLogix.Shared.Models
{
    public class Employee
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage ="Please Enter Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please Enter Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please Enter Birth Date")]
        public DateTime BirthDate { get; set; }
        [Required(ErrorMessage = "Please Enter Salary")]
        public int Salary { get; set; }
        public bool Status { get; set; }
    }
}
