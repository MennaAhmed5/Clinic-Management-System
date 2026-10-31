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

            if(scheduleVM.Doctor  == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(scheduleVM);
        }
    }
}
