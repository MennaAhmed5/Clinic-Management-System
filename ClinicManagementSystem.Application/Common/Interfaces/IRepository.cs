using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ClinicManagementSystem.Application.Common.Interfaces
{
    public interface IRepository<T> where T : class 
    {
        IEnumerable<T> GetAll(Expression<Func<T,bool>> filter = null, string? IncludeProperties= null, bool tracked = false);

        T Get(Expression<Func<T, bool>>? filter = null, string? IncludeProperties = null, bool tracked = false);

        void Add(T entity);

        void Remove (T entity);

        bool Any(Expression<Func<T, bool>> filter = null);
    }
}
