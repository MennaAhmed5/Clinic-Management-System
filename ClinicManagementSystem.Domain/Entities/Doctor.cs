using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ClinicManagementSystem.Domain.Entites
{
    public class Doctor
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string? Phone {  get; set; }

        public string? Specialization { get; set; }

        [Required]
        [ForeignKey("Clinic")]
        public int ClinicId { get; set; }
        [ValidateNever]
        public Clinic Clinic { get; set; }

        [ValidateNever]
        public IEnumerable<Schedule> Schedules { get; set; }
        [ValidateNever]
        public IEnumerable<Appointment> Appointments { get; set; }
    }
}
