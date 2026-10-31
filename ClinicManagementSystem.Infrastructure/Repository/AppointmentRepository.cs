using ClinicManagementSystem.Application.Interfaces;
using ClinicManagementSystem.Domain.Entites;
using ClinicManagementSystem.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClinicManagementSystem.Infrastructure.Repository
{
    public class AppointmentRepository : Repository<Appointment>, IAppointmentRepository
    {
        private readonly ApplicationDbContext _db;

        public AppointmentRepository(ApplicationDbContext db):base(db)
        {
            _db= db;    
        }
        public void Update(Appointment appointment)
        {
           _db.Update(appointment);  
        }
    }
}
