using ClinicManagementSystem.Application.Common.Interfaces;
using ClinicManagementSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ClinicManagementSystem.Infrastructure.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;

        public Repository(ApplicationDbContext db)
        {
            _db = db;
            dbSet = db.Set<T>();    
        }
        public void Add(T entity)
        {
            _db.Add(entity);
        }

        public bool Any(Expression<Func<T, bool>> filter = null)
        {
            return dbSet.Any(filter);   
        }

        public T Get(Expression<Func<T, bool>>? filter = null, string? IncludeProperties = null, bool tracked = false)
        {
            IQueryable<T> query;
            if (tracked)
            {
                query = dbSet;
            }
            else
            {
                query = dbSet.AsNoTracking();
            }
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (!string.IsNullOrEmpty(IncludeProperties))
            {
                 foreach(var includeProp in IncludeProperties.Split(new char[] { ','}, StringSplitOptions.RemoveEmptyEntries))
                 {
                    query = query.Include(includeProp.Trim());
                 }
            }

            return query.FirstOrDefault();

        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null, string? IncludeProperties = null, bool tracked = false)
        {
            IQueryable<T> query;
            if (tracked)
            {
                query = dbSet;
            }
            else
            {
                query = dbSet.AsNoTracking();
            }

            if(filter != null)
            {
                query = query.Where(filter);
            }

            //Appointment, Doctor --case senstive
            if (!string.IsNullOrEmpty(IncludeProperties))
            {
                foreach (var includeProp in IncludeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)) { 
                    query = query.Include(includeProp.Trim());
                }
            }
            return query.ToList();
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }
    }
}
