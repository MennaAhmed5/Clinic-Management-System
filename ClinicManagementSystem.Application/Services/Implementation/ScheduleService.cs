using ClinicManagementSystem.Application.Common.Interfaces;
using ClinicManagementSystem.Application.Interfaces;
using ClinicManagementSystem.Application.Services.Interfaces;
using ClinicManagementSystem.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClinicManagementSystem.Application.Services.Implementation
{
    public class ScheduleService : IScheduleService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ScheduleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void CreateSchedule(Schedule schedule)
        {
            _unitOfWork.Schedule.Add(schedule);
            _unitOfWork.Save();
        }

        public bool DeleteSchedule(int doctorId, DayOfWeek dayOfWeek)
        {
            Schedule? objFromDb = _unitOfWork.Schedule.Get(u => u.DoctorId == doctorId&& u.DayOfTheWeek == dayOfWeek);
            if (objFromDb is not null)
            {
                _unitOfWork.Schedule.Remove(objFromDb);
                _unitOfWork.Save();
                return true;
            }
            return false;
        }

        public IQueryable<Schedule> GetAllSchedules( int doctorId=0)
        {
            return _unitOfWork.Schedule.GetAll(filter: x=>x.DoctorId == doctorId);
        }

        public Schedule GetScheduleById(int doctorId, DayOfWeek dayOfWeek)
        {
            return _unitOfWork.Schedule.Get(u => u.DoctorId == doctorId && u.DayOfTheWeek == dayOfWeek);

        }

        public void UpdateSchedule(Schedule schedule)
        {
            _unitOfWork.Schedule.Update(schedule);
            _unitOfWork.Save();
        }
        public bool CheckScheduleExists(int doctorId, DayOfWeek day)
        {
            return _unitOfWork.Schedule.Any(u => u.DoctorId == doctorId && u.DayOfTheWeek == day);
        }

    }
}
