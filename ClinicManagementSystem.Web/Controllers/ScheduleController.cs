using ClinicManagementSystem.Application.Services.Implementation;
using ClinicManagementSystem.Application.Services.Interfaces;
using ClinicManagementSystem.Domain.Entites;
using ClinicManagementSystem.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ClinicManagementSystem.Web.Controllers
{
    public class ScheduleController : Controller
    {
        private readonly IScheduleService _scheduleService;
        private readonly IDoctorService _doctorService;
        public ScheduleController(IScheduleService scheduleService, IDoctorService doctorService)
        {
            _scheduleService = scheduleService;
            _doctorService = doctorService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetSchedulesByDoctorId(int doctorId, int pageNumber)
        {
            var Schedules = _scheduleService.GetAllSchedules(doctorId);

            if (pageNumber < 1)
            {
                pageNumber = 1;
            }
            int pageSize = 5;

            ScheduleVM scheduleVM = new()
            {
                PaginatedList = PaginatedList<Schedule>.Create(Schedules, pageNumber, pageSize),
                Doctor = _doctorService.GetDoctorById(doctorId)
            };

            if (scheduleVM.Doctor == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(scheduleVM);
        }
        [HttpPost]
        public IActionResult Create(CreateScheduleVM obj)
        {
            bool IsValidTimeRange = obj.StartTime < obj.EndTime;
            bool ScheduleExist = _scheduleService.CheckScheduleExists(obj.DoctorId, obj.DayOfTheWeek);


            if (ModelState.IsValid && IsValidTimeRange && !ScheduleExist)
            {
                var schedule = new Schedule
                {
                    DoctorId = obj.DoctorId,
                    DayOfTheWeek = obj.DayOfTheWeek,
                    StartTime = obj.StartTime,
                    EndTime = obj.EndTime
                };
                _scheduleService.CreateSchedule(schedule);

                return Json(new
                {
                    success = true,
                    message = "The schedule has been created successfully."
                });
            }
            if (!IsValidTimeRange)
            {
                ModelState.AddModelError("EndTime", "End time must be greater than start time.");
            }
            if (ScheduleExist)
            {
                ModelState.AddModelError("DayOfTheWeek", "A schedule already exists for this doctor on the selected day.");
            }

            return PartialView(
           "~/Views/Schedule/Partials/_CreateScheduleModal.cshtml",
           obj);

        }

        public IActionResult Update(DayOfWeek dayOfTheWeek, int doctorId)
        {
            Schedule? obj = _scheduleService.GetScheduleById(doctorId, dayOfTheWeek);
            if (obj == null)
            {
                return RedirectToAction("Error", "Home");
            }
            CreateScheduleVM createScheduleVM = new()
            {
                DayOfTheWeek = obj.DayOfTheWeek,
                StartTime = obj.StartTime,
                EndTime = obj.EndTime,
                DoctorId = obj.DoctorId
            };
            return PartialView("~/Views/Schedule/Partials/_UpdateScheduleModal.cshtml", createScheduleVM);
        }


        [HttpPost]
        public IActionResult Update(CreateScheduleVM obj)
        {
            bool IsValidTimeRange = obj.StartTime < obj.EndTime;

            if (ModelState.IsValid && IsValidTimeRange)
            {
                var schedule = new Schedule
                {
                    DoctorId = obj.DoctorId,
                    DayOfTheWeek = obj.DayOfTheWeek,
                    StartTime = obj.StartTime,
                    EndTime = obj.EndTime
                };
                _scheduleService.UpdateSchedule(schedule);

                return Json(new
                {
                    success = true,
                    message = "The schedule has been updated successfully."
                });
            }
            if (!IsValidTimeRange)
            {
                ModelState.AddModelError("EndTime", "End time must be greater than start time.");
            }

            return PartialView(
           "~/Views/Schedule/Partials/_UpdateScheduleModal.cshtml",
           obj);

        }

        public IActionResult Delete(DayOfWeek dayOfTheWeek, int doctorId)
        {
            Schedule? obj = _scheduleService.GetScheduleById(doctorId, dayOfTheWeek);
            if (obj == null)
            {
                return RedirectToAction("Error", "Home");
            }
            CreateScheduleVM createScheduleVM = new()
            {
                DayOfTheWeek = obj.DayOfTheWeek,
                StartTime = obj.StartTime,
                EndTime = obj.EndTime,
                DoctorId = obj.DoctorId
            };
            return PartialView("~/Views/Schedule/Partials/_DeleteScheduleModal.cshtml", createScheduleVM);
        }

        [HttpPost]
        public IActionResult DeleteSchedule(DayOfWeek dayOfTheWeek, int doctorId)
        {
            bool deleted = _scheduleService.DeleteSchedule(doctorId, dayOfTheWeek);
            if (deleted)
            {
                return Json(new
                {
                    success = true,
                    message = "The schedule has been deleted successfully"
                });

            }
            return Json(new
            {
                success = false,
                message = "Error..  cannot delete schedule"
            });


        }
        /// <summary>
        /// this action for enabled and disabled days in the AppointmentDate picker
        /// </summary>
        /// <param name="doctorId"></param>
        /// <returns></returns>

        public IActionResult GetDoctorSchedules(int doctorId)
        {
            var schedules = _scheduleService.GetAllSchedules(doctorId).ToList();
            return Json(new { data = schedules, success = true });
        }
    }
}
