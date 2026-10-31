using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ClinicManagementSystem.Domain.Entites
{
    public class Clinic
    {
        [Key]
        public int Id { get; set; }

        [Required]

        public string Name { get; set; }

        public string? Phone { get; set; }

        public string? Address { get; set;  }

        [ValidateNever]
        public IEnumerable<Doctor> Doctors { get; set; } 

    }
}
