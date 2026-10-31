using ClinicManagementSystem.Application.Common.Interfaces;
using ClinicManagementSystem.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClinicManagementSystem.Application.Interfaces
{
    public interface IAppointmentRepository: IRepository<Appointment>
    {
        void Update (Appointment appointment);    
    }
}
