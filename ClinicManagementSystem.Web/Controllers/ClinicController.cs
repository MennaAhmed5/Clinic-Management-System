using ClinicManagementSystem.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagementSystem.Web.Controllers
{
    public class ClinicController : Controller
    {
        private readonly IClinicService _ClinicService;
        public ClinicController(IClinicService clinicService) 
        {
            _ClinicService = clinicService; 
        }   
        public IActionResult Index()
        {
            var clinics = _ClinicService.GetAllClinics();
            return View(clinics);
        }
    }
}
