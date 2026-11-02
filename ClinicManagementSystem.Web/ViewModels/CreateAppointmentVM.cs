using ClinicManagementSystem.Domain.Entites;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ClinicManagementSystem.Web.ViewModels
{
    public class CreateAppointmentVM
    {
        public Patient Patient { get; set; }

        public Appointment Appointment { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> DoctorsList { get; set; }

    }
}
