using ClinicManagementSystem.Application.Services.Implementation;
using ClinicManagementSystem.Application.Services.Interfaces;
using ClinicManagementSystem.Domain.Entites;
using ClinicManagementSystem.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ClinicManagementSystem.Web.Controllers
{
    public class DoctorController : Controller
    {
        private readonly IDoctorService _doctorService;
        private readonly IClinicService _clinicService;
        public DoctorController(IDoctorService doctorService, IClinicService clinicService) 
        {
            _doctorService = doctorService;
            _clinicService = clinicService; 
        }
        public IActionResult Index(int pageNumber)
        {
            var doctors = _doctorService.GetAllDoctors();
            if (pageNumber < 1)
            {
                pageNumber = 1;
            }
            int pageSize = 5;
            var paginatedList = PaginatedList<Doctor>.Create(doctors, pageNumber, pageSize);
            return View(paginatedList);
        }
        public IActionResult Create()
        {
            DoctorVM doctorVM = new()
            {
                ClinicList = _clinicService.GetAllClinics().Select(x => new SelectListItem()
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                })
            };
            return View(doctorVM);
        }
        [HttpPost]
        public IActionResult Create(DoctorVM obj)
        {
            bool phoneNumberExists = false;
            if (obj.Doctor.Phone != null) {
                phoneNumberExists = _doctorService.CheckPhoneNumberExists(obj.Doctor.Phone);
            }

            if (ModelState.IsValid && !phoneNumberExists)
            {
                _doctorService.CreateDoctor(obj.Doctor);

                TempData["success"] = "The doctor has been created successfully.";

                return RedirectToAction(nameof(Index));
            }

            if (phoneNumberExists)
            {
                ModelState.AddModelError("Doctor.Phone", "This phone number is already registered.");
 
            }
            obj.ClinicList = _clinicService.GetAllClinics().Select(u => new SelectListItem()
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });

            return View(obj);
        }

        public IActionResult Update(int doctorId)
        {
            DoctorVM doctorVM = new()
            {
                ClinicList = _clinicService.GetAllClinics().Select(x => new SelectListItem()
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }),
                Doctor = _doctorService.GetDoctorById(doctorId)
                
            };

            if(doctorVM.Doctor == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(doctorVM);
        }

        [HttpPost]
        public IActionResult Update(DoctorVM obj)
        {
            bool phoneNumberExists = false;
            if (obj.Doctor.Phone != null)
            {
                phoneNumberExists = _doctorService.CheckPhoneNumberExists(obj.Doctor.Phone, obj.Doctor.Id);
            }

            if (ModelState.IsValid && !phoneNumberExists)
            {
                _doctorService.UpdateDoctor(obj.Doctor);

                TempData["success"] = "The doctor has been Updated successfully.";

                return RedirectToAction(nameof(Index));
            }

            if (phoneNumberExists)
            {
                ModelState.AddModelError("Doctor.Phone", "This phone number is already registered.");

            }
            obj.ClinicList = _clinicService.GetAllClinics().Select(u => new SelectListItem()
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });

            return View(obj);
        }

        public IActionResult Delete(int doctorId)
        {
            DoctorVM doctorVM = new()
            {
                ClinicList = _clinicService.GetAllClinics().Select(x => new SelectListItem()
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }),
                Doctor = _doctorService.GetDoctorById(doctorId)

            };

            if (doctorVM.Doctor == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(doctorVM);
        }

        [HttpPost]
        public IActionResult Delete(Doctor doctor)
        {
            bool deleted = _doctorService.DeleteDoctor(doctor.Id);
            if (deleted)
            {
                TempData["success"] = "The doctor has been deleted successfully.";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["error"] = "The doctor could not be deleted";
            }
            return View();
        }
    }
}
