using ClinicManagementSystem.Application.Services.Implementation;
using ClinicManagementSystem.Application.Services.Interfaces;
using ClinicManagementSystem.Domain.Entites;
using ClinicManagementSystem.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ClinicManagementSystem.Web.Controllers
{
    [Authorize]
    public class PatientController : Controller
    {
        private readonly IPatientService _patientService;
        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }
        public IActionResult Index(int pageNumber)
        {
            var patients = _patientService.GetAllPatients();
            if (pageNumber < 1)
            {
                pageNumber = 1;
            }
            int pageSize = 5;
            var paginatedList = PaginatedList<Patient>.Create(patients, pageNumber, pageSize);
            return View(paginatedList);
        }

        public IActionResult Update(int patientId)
        {
            Patient? obj = _patientService.GetPatientById(patientId);
            if (obj == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(obj);
        }
        [HttpPost]
        public IActionResult Update(Patient obj)
        {
            bool phoneNumberExists = false;
            if (obj.Phone != null)
            {
                phoneNumberExists = _patientService.CheckPhoneNumberExists(obj.Phone, obj.Id);
            }

            if (ModelState.IsValid && obj.Id > 0 && !phoneNumberExists)
            {
                _patientService.UpdatePatient(obj);
                TempData["success"] = "The patient has been Updated successfully.";
                return RedirectToAction(nameof(Index));
            }
            if (phoneNumberExists)
            {
                ModelState.AddModelError("Phone", "Another patient already has this phone number.");
            }
            return View(obj);
        }
        public IActionResult Delete(int patientId)
        {
            Patient? obj = _patientService.GetPatientById(patientId);
            if (obj == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(obj);
        }
        [HttpPost]
        public IActionResult Delete(Patient patient)
        {
            bool deleted = _patientService.DeletePatient(patient.Id);
            if (deleted)
            {
                TempData["success"] = "The patient has been deleted successfully.";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["error"] = "The patient could not be deleted";
            }
            return View();
        }
    }
}
