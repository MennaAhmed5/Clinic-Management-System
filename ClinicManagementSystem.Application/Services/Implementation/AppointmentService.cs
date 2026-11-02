using ClinicManagementSystem.Application.Common.Interfaces;
using ClinicManagementSystem.Application.Interfaces;
using ClinicManagementSystem.Application.Services.Interfaces;
using ClinicManagementSystem.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClinicManagementSystem.Application.Services.Implementation
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AppointmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void CreateAppointment(Appointment appointment)
        {
            _unitOfWork.Appointment.Add(appointment);
            _unitOfWork.Save();
        }

        public bool DeleteAppointment(int id)
        {
            Appointment? objFromDb = _unitOfWork.Appointment.Get(u => u.Id == id);
            if (objFromDb is not null)
            {
                _unitOfWork.Appointment.Remove(objFromDb);
                _unitOfWork.Save();
                return true;
            }
            return false;
        }

        public IQueryable<Appointment> GetAllAppointments()
        {
            return _unitOfWork.Appointment.GetAll();
        }

        public Appointment GetAppointmentById(int id)
        {
            return _unitOfWork.Appointment.Get(u => u.Id == id);

        }

        public IQueryable<Appointment> GetAppointmentsBydoctorId(int doctorId)
        {
            return _unitOfWork.Appointment.GetAll(u=>u.DoctorId == doctorId);
        }

        public List<TimeOnly> GetAvailableTimeSlots(int doctorId, DateOnly date)
        {
            const int duration = 30;
            //get the doctor schedule 
            var doctorSchedule = _unitOfWork.Schedule.Get(u => u.DoctorId == doctorId && u.DayOfTheWeek == date.DayOfWeek);

            if (doctorSchedule == null)
                return new List<TimeOnly>();

            //get booked time slots from  doctor appointment
            var bookedSlots = _unitOfWork.Appointment.GetAll(u=>u.DoctorId ==doctorId && u.AppointmentDate == date).Select(u => u.AppointmentTime);

            var allSlots = new List<TimeOnly>();
            
            //generate all time slots 
            for (var time = doctorSchedule.StartTime;
                time.AddMinutes(duration) <= doctorSchedule.EndTime;
                 time = time.AddMinutes(duration))
            {
                allSlots.Add(time);
            }
            return allSlots.Except(bookedSlots).ToList();
        }

        public void UpdateAppointment(Appointment appointment)
        {
            _unitOfWork.Appointment.Update(appointment);
            _unitOfWork.Save();
        }
        

    }
}
