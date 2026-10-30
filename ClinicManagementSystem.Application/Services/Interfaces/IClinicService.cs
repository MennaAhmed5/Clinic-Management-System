using ClinicManagementSystem.Application.Common.Interfaces;
using ClinicManagementSystem.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClinicManagementSystem.Application.Services.Interfaces
{
    public interface IClinicService
    {
        IEnumerable<Clinic> GetAllClinics();
        Clinic GwtClinicById(int id);

        void CreateClinic(Clinic clinic);

        void UpdateClinic(Clinic clinic);

        bool DeleteClinic(int id);


    }
}
