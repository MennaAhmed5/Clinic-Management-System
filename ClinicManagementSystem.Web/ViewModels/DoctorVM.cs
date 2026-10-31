using ClinicManagementSystem.Domain.Entites;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ClinicManagementSystem.Web.ViewModels
{
    public class DoctorVM
    {
        public Doctor? Doctor { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> ClinicList { get; set; }
    }
}
