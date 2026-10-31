using ClinicManagementSystem.Application.Common.Interfaces;
using ClinicManagementSystem.Application.Interfaces;
using ClinicManagementSystem.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClinicManagementSystem.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        public IClinicRepository Clinic { get; private set; }

        public IDoctorRepository Doctor { get; private set; }

        public IScheduleRepository Schedule { get; private set; }
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Clinic = new ClinicRepository(_db);
            Doctor = new DoctorRepository(_db);
            Schedule = new ScheduleRepository(_db); 
        }
        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
