using ClinicManagementSystem.Application.Interfaces;
using ClinicManagementSystem.Domain.Entites;
using ClinicManagementSystem.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClinicManagementSystem.Infrastructure.Repository
{
    public class PatientRepository : Repository<Patient>, IPatientRepository
    {
        private readonly ApplicationDbContext _db;

        public PatientRepository(ApplicationDbContext db):base(db)
        {
            _db= db;    
        }
        public void Update(Patient patient)
        {
           _db.Update(patient);  
        }
    }
}
