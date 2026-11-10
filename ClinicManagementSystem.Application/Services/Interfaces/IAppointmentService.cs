using ClinicManagementSystem.Application.Common.Interfaces;
using ClinicManagementSystem.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClinicManagementSystem.Application.Services.Interfaces
{
    public interface IAppointmentService
    {
        IQueryable<Appointment> GetAllAppointments();
        Appointment GetAppointmentById(int id);

        void CreateAppointment(Appointment Appointment);

        void UpdateAppointment(Appointment Appointment);

        bool DeleteAppointment(int id);

        IQueryable<Appointment> GetAppointmentsBydoctorId(int doctorId);

        List<TimeOnly> GetAvailableTimeSlots(int doctorId, DateOnly date);

        bool checkPatientDate(DateOnly appointmentDate, TimeOnly appointmentTime, int patientId);


    }
}
