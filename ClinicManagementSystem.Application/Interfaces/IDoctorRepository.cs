using ClinicManagementSystem.Application.Common.Interfaces;
using ClinicManagementSystem.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClinicManagementSystem.Application.Interfaces
{
    public interface IDoctorRepository: IRepository<Doctor>
    {
        void Update (Doctor doctor);    
    }
}
