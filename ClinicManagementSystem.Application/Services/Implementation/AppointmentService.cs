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

        public void UpdateAppointment(Appointment appointment)
        {
            _unitOfWork.Appointment.Update(appointment);
            _unitOfWork.Save();
        }
        

    }
}
