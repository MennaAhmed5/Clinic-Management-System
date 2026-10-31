using ClinicManagementSystem.Application.Services.Interfaces;
using ClinicManagementSystem.Domain.Entites;
using ClinicManagementSystem.Web.ViewModels;
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
        public IActionResult Index(int pageNumber)
        {
            var clinics = _ClinicService.GetAllClinics();
            if (pageNumber < 1)
            {
                pageNumber = 1;
            }
            int pageSize = 5;
            var paginatedList = PaginatedList<Clinic>.Create(clinics, pageNumber, pageSize);
            return View(paginatedList);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Clinic clinic)
        {
            if (ModelState.IsValid)
            {
                _ClinicService.CreateClinic(clinic);
                TempData["success"] = "The Clinic has been created successfully.";
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        public IActionResult Update(int clinicId)
        {
            Clinic? obj = _ClinicService.GetClinicById(clinicId);
            if(obj == null)
            {
               return RedirectToAction("Error", "Home");
            }
            return View(obj);
        }
        [HttpPost]
        public IActionResult Update(Clinic obj)
        {
            if(ModelState.IsValid &&  obj.Id > 0)
            {
                _ClinicService.UpdateClinic(obj);
                TempData["success"] = "The villa has been Updated successfully.";
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        public IActionResult Delete(int clinicId)
        {
            Clinic? obj = _ClinicService.GetClinicById(clinicId);
            if(obj == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(obj);
        }
        [HttpPost]
        public IActionResult Delete(Clinic clinic)
        {
            bool deleted = _ClinicService.DeleteClinic(clinic.Id);
            if (deleted)
            {
                TempData["success"] = "The clinic has been deleted successfully.";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["error"] = "The clinic could not be deleted";
            }
            return View();
        }
    }
}
