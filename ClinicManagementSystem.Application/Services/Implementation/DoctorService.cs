using ClinicManagementSystem.Application.Common.Interfaces;
using ClinicManagementSystem.Application.Interfaces;
using ClinicManagementSystem.Application.Services.Interfaces;
using ClinicManagementSystem.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClinicManagementSystem.Application.Services.Implementation
{
    public class DoctorService : IDoctorService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DoctorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void CreateDoctor(Doctor doctor)
        {
            _unitOfWork.Doctor.Add(doctor);
            _unitOfWork.Save();
        }

        public bool DeleteDoctor(int id)
        {
            Doctor? objFromDb = _unitOfWork.Doctor.Get(u => u.Id == id);
            if (objFromDb is not null)
            {
                _unitOfWork.Doctor.Remove(objFromDb);
                _unitOfWork.Save();
                return true;
            }
            return false;
        }

        public IQueryable<Doctor> GetAllDoctors()
        {
            return _unitOfWork.Doctor.GetAll(IncludeProperties:"Clinic");
        }

        public Doctor GetDoctorById(int id)
        {
            return _unitOfWork.Doctor.Get(u => u.Id == id);

        }

        public void UpdateDoctor(Doctor doctor)
        {
            _unitOfWork.Doctor.Update(doctor);
            _unitOfWork.Save();
        }
        public bool CheckPhoneNumberExists(string PhoneNumber, int doctorId=0)
        {
            return _unitOfWork.Doctor.Any(u => u.Phone == PhoneNumber && u.Id != doctorId);
        }

    }
}
