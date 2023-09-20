using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeLogix.Shared.Dtos
{
    public class LoginResponse
    {
        public bool isSuccessfull { get; set; }
        public IEnumerable<string> errors { get; set; }
        public string Token { get; set; }
    }
}
