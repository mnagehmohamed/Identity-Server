﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeLogix.Shared.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string? Country { get; set; }
        public string? Address { get; set; }
    }
}
