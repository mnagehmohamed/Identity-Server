using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeLogix.Shared.Dtos
{
    public class RegistrationResponse
    {
        public bool isSuccessfull { get; set; }
        public IEnumerable<string> errors { get; set; }
    }
}
