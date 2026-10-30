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
        public string? Phone {  get; set; }

        public string? Specialization { get; set; }

        [ForeignKey("Clinic")]
        public int ClinicId { get; set; }   

        public Clinic Clinic { get; set; }

        public IEnumerable<Schedule> Schedules { get; set; }

        public IEnumerable<Appointment> Appointments { get; set; }
    }
}
