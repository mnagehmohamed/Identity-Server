using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeLogix.Shared.Dtos
{
    public class RegistrationUser
    {
        [Required(ErrorMessage ="Please Insert Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please Insert User Name")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Please Insert Password")]
        public string Password { get; set; }
        [Compare("Password",ErrorMessage ="Password Dont Match")]
        public string ConfirmPassword { get; set; }
    }
}
