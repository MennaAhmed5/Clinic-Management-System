using ClinicManagementSystem.Application.Common.Interfaces;
using ClinicManagementSystem.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClinicManagementSystem.Application.Services.Interfaces
{
    public interface IPatientService
    {
        IQueryable<Patient> GetAllPatients();
        Patient GetPatientById(int id);

        Patient GetPatientByPhoneNumber(string phoneNumber);
        void CreatePatient(Patient Patient);

        void UpdatePatient(Patient Patient);

        bool DeletePatient(int id);

        public bool CheckPhoneNumberExists(string PhoneNumber);
    }
}
