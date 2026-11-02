using ClinicManagementSystem.Application.Services.Implementation;
using ClinicManagementSystem.Application.Services.Interfaces;
using ClinicManagementSystem.Domain.Entites;
using ClinicManagementSystem.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ClinicManagementSystem.Web.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly IDoctorService _doctorService;
        private readonly IAppointmentService _appointmentService;
        private readonly IPatientService _patientService;
        private readonly IScheduleService _scheduleService;

        public AppointmentController(IDoctorService doctorService, IAppointmentService appointmentService, IPatientService patientService, IScheduleService scheduleService)
        {
            _doctorService = doctorService;
            _appointmentService = appointmentService;
            _patientService = patientService;
            _scheduleService = scheduleService; 
        }
        public IActionResult Index()
        {
            return View();
        }
       
        public IActionResult Create()
        {
            CreateAppointmentVM createAppointmentVM = new()
            {
                DoctorsList = _doctorService.GetAllDoctors().Select(u => new SelectListItem()
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                })
            };
                 
            return View(createAppointmentVM);
        }

      
        [HttpPost]
        public IActionResult Create(CreateAppointmentVM obj)
        {
            if (!ModelState.IsValid)
                return View(obj);

            // Check if the doctor works on the selected day
            var doctorSchedules = _scheduleService.GetAllSchedules(
                obj.Appointment.DoctorId
            );

            bool isDayAvailable = doctorSchedules.Any(
                u => u.DayOfTheWeek == obj.Appointment.AppointmentDate.DayOfWeek
            );

            // Check if the selected time is available
            var availableTimeSlots = _appointmentService.GetAvailableTimeSlots(
                obj.Appointment.DoctorId,
                obj.Appointment.AppointmentDate
            );

            bool isTimeAvailable = availableTimeSlots.Contains(
                obj.Appointment.AppointmentTime
            );

            if (!isDayAvailable)
            {
                ModelState.AddModelError(
                    "Appointment.AppointmentDate",
                    "The doctor doesn't work on this day."
                );
            }

            if (!isTimeAvailable)
            {
                ModelState.AddModelError(
                    "Appointment.AppointmentTime",
                    "The selected time is not available."
                );
            }

            if (!ModelState.IsValid)
                return View(obj);

            // Get existing patient
            var patient = _patientService.GetPatientByPhoneNumber(
                obj.Patient.Phone
            );

            // Create patient if not exists
            if (patient == null)
            {
                patient = obj.Patient;
                _patientService.CreatePatient(patient);
            }

            // Link appointment to patient
            obj.Appointment.PatientId = patient.Id;

            // Save appointment
            try
            {
                _appointmentService.CreateAppointment(obj.Appointment);

                TempData["success"] = "The Appointment has been created successfully.";
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError(
                    "Appointment.AppointmentTime",
                    "This time has just been booked. Please select another time."
                );

                // Reload doctors dropdown before returning the view
                obj.DoctorsList = _doctorService.GetAllDoctors().Select(u => new SelectListItem()
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });

                return View(obj);
            }


        }

        public IActionResult GetAvailableTimeSlots(int doctorId, DateOnly date)
        {
            var availableTimeSlots = _appointmentService.GetAvailableTimeSlots(doctorId,date);
            return Json(new { data = availableTimeSlots, success = true });
        }



    }
}
