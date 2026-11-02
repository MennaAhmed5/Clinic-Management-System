using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClinicManagementSystem.Domain.Entites
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public DateTime CrearedAt { get; set; }
    }
}
