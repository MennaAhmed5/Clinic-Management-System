using ClinicManagementSystem.Domain.Entites;

namespace ClinicManagementSystem.Web.ViewModels
{
    public class ScheduleVM
    {
        public Doctor Doctor { get; set; }
        public PaginatedList<Schedule> PaginatedList { get; set; }
    }
}
