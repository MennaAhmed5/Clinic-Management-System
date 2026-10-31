using ClinicManagementSystem.Application.Interfaces;
using ClinicManagementSystem.Domain.Entites;
using ClinicManagementSystem.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClinicManagementSystem.Infrastructure.Repository
{
    public class DoctorRepository : Repository<Doctor>, IDoctorRepository
    {
        private readonly ApplicationDbContext _db;

        public DoctorRepository(ApplicationDbContext db):base(db)
        {
            _db= db;    
        }
        public void Update(Doctor doctor)
        {
           _db.Update(doctor);  
        }
    }
}
