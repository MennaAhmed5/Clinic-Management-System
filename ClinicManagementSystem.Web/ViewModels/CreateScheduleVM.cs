using System.ComponentModel.DataAnnotations;

namespace ClinicManagementSystem.Web.ViewModels
{
    public class CreateScheduleVM
    {
        [Required]
        public DayOfWeek DayOfTheWeek { get; set; }
        [Required]
         
        public int DoctorId { get; set; }
        [Required]
        public TimeOnly StartTime { get; set; }
        [Required]
        public TimeOnly EndTime { get; set; }
    }
}
