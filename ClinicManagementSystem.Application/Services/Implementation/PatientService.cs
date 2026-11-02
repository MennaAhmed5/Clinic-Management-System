using ClinicManagementSystem.Application.Common.Interfaces;
using ClinicManagementSystem.Application.Interfaces;
using ClinicManagementSystem.Application.Services.Interfaces;
using ClinicManagementSystem.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClinicManagementSystem.Application.Services.Implementation
{
    public class PatientService :IPatientService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PatientService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void CreatePatient(Patient patient)
        {
            _unitOfWork.Patient.Add(patient);
            _unitOfWork.Save();
        }

        public bool DeletePatient(int id)
        {
            Patient? objFromDb = _unitOfWork.Patient.Get(u => u.Id == id);
            if (objFromDb is not null)
            {
                _unitOfWork.Patient.Remove(objFromDb);
                _unitOfWork.Save();
                return true;
            }
            return false;
        }

        public IQueryable<Patient> GetAllPatients()
        {
            return _unitOfWork.Patient.GetAll();
        }

        public Patient GetPatientById(int id)
        {
            return _unitOfWork.Patient.Get(u => u.Id == id);

        }

        public void UpdatePatient(Patient patient)
        {
            _unitOfWork.Patient.Update(patient);
            _unitOfWork.Save();
        }
        public bool CheckPhoneNumberExists(string PhoneNumber, int patientId = 0)
        {
            return _unitOfWork.Patient.Any(u => u.Phone == PhoneNumber && (patientId == 0 || u.Id != patientId));
        }

        public Patient GetPatientByPhoneNumber(string phoneNumber)
        {
            return _unitOfWork.Patient.Get(u => u.Phone == phoneNumber);
        }
    }
}
