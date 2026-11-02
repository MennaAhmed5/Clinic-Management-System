using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ClinicManagementSystem.Domain.Entites
{
    public class Schedule
    {
         
        [Required]
        public DayOfWeek DayOfTheWeek { get; set; }
        [Required]
        [ForeignKey("Doctor")]
        public int DoctorId { get; set; }
        [Required]
        public TimeOnly StartTime { get; set; }
        [Required]
        public TimeOnly EndTime { get; set; }

        [ValidateNever]
        public Doctor Doctor { get; set; }

     }
}
