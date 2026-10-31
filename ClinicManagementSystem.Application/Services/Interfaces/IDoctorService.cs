using ClinicManagementSystem.Application.Common.Interfaces;
using ClinicManagementSystem.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClinicManagementSystem.Application.Services.Interfaces
{
    public interface IDoctorService
    {
        IQueryable<Doctor> GetAllDoctors();
        Doctor GetDoctorById(int id);

        void CreateDoctor(Doctor Doctor);

        void UpdateDoctor(Doctor Doctor);

        bool DeleteDoctor(int id);

        public bool CheckPhoneNumberExists(string PhoneNumber, int doctorId=0);
    }
}
