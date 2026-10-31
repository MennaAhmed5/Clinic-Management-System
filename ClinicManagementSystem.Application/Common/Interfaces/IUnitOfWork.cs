using ClinicManagementSystem.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClinicManagementSystem.Application.Common.Interfaces
{
    public interface IUnitOfWork
    {
        public IClinicRepository Clinic { get; }
        
        public IDoctorRepository Doctor { get; }   
        
        public IScheduleRepository Schedule { get; }
        public void Save();

    }
}
