using ClinicManagementSystem.Application.Common.Interfaces;
using ClinicManagementSystem.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClinicManagementSystem.Application.Services.Interfaces
{
    public interface IScheduleService
    {
        IQueryable<Schedule> GetAllSchedules(int doctorId = 0);
        Schedule GetScheduleById(int doctorId, DayOfWeek dayOfWeek);

        void CreateSchedule(Schedule schedule);

        void UpdateSchedule(Schedule schedule);

        bool DeleteSchedule(int doctorId, DayOfWeek dayOfWeek);


    }
}
