using ClinicManagementSystem.Domain.Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClinicManagementSystem.Infrastructure.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Clinic> Clinics { get; set; }
        public DbSet<Doctor> Doctors { get; set; }

        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Patient> Patients { get; set; }

        public DbSet<Appointment> Appointments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // create a composite key 
            modelBuilder.Entity<Schedule>().HasKey(s => new { s.DoctorId, s.DayOfTheWeek });

            base.OnModelCreating(modelBuilder);

            #region seeding


            // Composite Primary Key
            modelBuilder.Entity<Schedule>()
                .HasKey(s => new { s.DoctorId, s.DayOfTheWeek });

            // Clinics
            modelBuilder.Entity<Clinic>().HasData(
                new Clinic
                {
                    Id = 1,
                    Name = "Smile Dental Clinic",
                    Phone = "0223456789",
                    Address = "Nasr City, Cairo"
                },
                new Clinic
                {
                    Id = 2,
                    Name = "Life Medical Center",
                    Phone = "0223456790",
                    Address = "Maadi, Cairo"
                },
                new Clinic
                {
                    Id = 3,
                    Name = "Care Clinic",
                    Phone = "0223456791",
                    Address = "Heliopolis, Cairo"
                }
            );

            // Doctors
            modelBuilder.Entity<Doctor>().HasData(
                new Doctor
                {
                    Id = 1,
                    Name = "Ahmed Mohamed",
                    Phone = "01012345678",
                    Specialization = "Dentist",
                    ClinicId = 1
                },
                new Doctor
                {
                    Id = 2,
                    Name = "Mona Ali",
                    Phone = "01112345678",
                    Specialization = "Cardiologist",
                    ClinicId = 2
                },
                new Doctor
                {
                    Id = 3,
                    Name = "Omar Hassan",
                    Phone = "01212345678",
                    Specialization = "Dermatologist",
                    ClinicId = 3
                }
            );

            // Schedules
            modelBuilder.Entity<Schedule>().HasData(
                new Schedule
                {
                    DoctorId = 1,
                    DayOfTheWeek = DayOfWeek.Saturday,
                    StartTime = new TimeOnly(16, 0),
                    EndTime = new TimeOnly(20, 0)
                },
                new Schedule
                {
                    DoctorId = 2,
                    DayOfTheWeek = DayOfWeek.Sunday,
                    StartTime = new TimeOnly(10, 0),
                    EndTime = new TimeOnly(14, 0)
                },
                new Schedule
                {
                    DoctorId = 3,
                    DayOfTheWeek = DayOfWeek.Monday,
                    StartTime = new TimeOnly(17, 0),
                    EndTime = new TimeOnly(21, 0)
                }
            );

            // Patients
            modelBuilder.Entity<Patient>().HasData(
                new Patient
                {
                    Id = 1,
                    Name = "Mohamed Ali",
                    DateOfBirth = new DateOnly(1995, 5, 10),
                    Gender = Gender.Male,
                    Phone = "01012345678",
                    Email = "mohamed@gmail.com",
                    Address = "Cairo"
                },
                new Patient
                {
                    Id = 2,
                    Name = "Sara Ahmed",
                    DateOfBirth = new DateOnly(2000, 8, 15),
                    Gender = Gender.Female,
                    Phone = "01112345678",
                    Email = "sara@gmail.com",
                    Address = "Giza"
                },
                new Patient
                {
                    Id = 3,
                    Name = "Ali Hassan",
                    DateOfBirth = new DateOnly(1988, 3, 20),
                    Gender = Gender.Male,
                    Phone = "01212345678",
                    Email = "ali@gmail.com",
                    Address = "Nasr City"
                }
            );

            // Appointments
            modelBuilder.Entity<Appointment>().HasData(
                new Appointment
                {
                    Id = 1,
                    PatientId = 1,
                    DoctorId = 1,
                    AppointmentDate = new DateOnly(2026, 11, 7),
                    AppointmentTime = new TimeOnly(16, 30)
                },
                new Appointment
                {
                    Id = 2,
                    PatientId = 2,
                    DoctorId = 2,
                    AppointmentDate = new DateOnly(2026, 11, 8),
                    AppointmentTime = new TimeOnly(10, 30)
                },
                new Appointment
                {
                    Id = 3,
                    PatientId = 3,
                    DoctorId = 3,
                    AppointmentDate = new DateOnly(2026, 11, 9),
                    AppointmentTime = new TimeOnly(17, 30)
                }
            );
        }

            #endregion

    }
}
