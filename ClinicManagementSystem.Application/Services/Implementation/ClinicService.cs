using ClinicManagementSystem.Application.Common.Interfaces;
using ClinicManagementSystem.Application.Interfaces;
using ClinicManagementSystem.Application.Services.Interfaces;
using ClinicManagementSystem.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClinicManagementSystem.Application.Services.Implementation
{
    public class ClinicService : IClinicService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ClinicService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void CreateClinic(Clinic clinic)
        {
            _unitOfWork.Clinic.Add(clinic);
            _unitOfWork.Save();
        }

        public bool DeleteClinic(int id)
        {
            Clinic? objFromDb = _unitOfWork.Clinic.Get(u => u.Id == id);
            if (objFromDb is not null)
            {
                _unitOfWork.Clinic.Remove(objFromDb);
                _unitOfWork.Save();
                return true;
            }
            return false;
        }

        public IEnumerable<Clinic> GetAllClinics()
        {
            return _unitOfWork.Clinic.GetAll();
        }

        public Clinic GwtClinicById(int id)
        {
            return _unitOfWork.Clinic.Get(u => u.Id == id);

        }

        public void UpdateClinic(Clinic clinic)
        {
            _unitOfWork.Clinic.Update(clinic);
            _unitOfWork.Save();
        }
    }
}
