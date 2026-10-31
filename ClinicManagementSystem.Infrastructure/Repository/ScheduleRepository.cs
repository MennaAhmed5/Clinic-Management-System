using ClinicManagementSystem.Application.Interfaces;
using ClinicManagementSystem.Domain.Entites;
using ClinicManagementSystem.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClinicManagementSystem.Infrastructure.Repository
{
    public class ScheduleRepository : Repository<Schedule>, IScheduleRepository
    {
        private readonly ApplicationDbContext _db;

        public ScheduleRepository(ApplicationDbContext db):base(db)
        {
            _db= db;    
        }
        public void Update(Schedule schedule)
        {
           _db.Update(schedule);  
        }
    }
}
