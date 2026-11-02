using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ClinicManagementSystem.Domain.Entites
{
    public class Appointment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateOnly AppointmentDate { get; set; }
        [Required]
        public TimeOnly AppointmentTime { get; set; }


        [ForeignKey("Patient")]
        public int PatientId { get; set; }


        [ForeignKey("Doctor")]
        public int DoctorId { get; set; }

        [ValidateNever]
        public Patient Patient { get; set; }

        [ValidateNever]
        public Doctor Doctor { get; set; }  

    }
}
