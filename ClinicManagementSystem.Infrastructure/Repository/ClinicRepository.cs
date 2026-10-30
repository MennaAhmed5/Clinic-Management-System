using ClinicManagementSystem.Application.Interfaces;
using ClinicManagementSystem.Domain.Entites;
using ClinicManagementSystem.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClinicManagementSystem.Infrastructure.Repository
{
    public class ClinicRepository : Repository<Clinic>, IClinicRepository
    {
        private readonly ApplicationDbContext _db;

        public ClinicRepository(ApplicationDbContext db):base(db)
        {
            _db= db;    
        }
        public void Update(Clinic clinic)
        {
           _db.Update(clinic);  
        }
    }
}
