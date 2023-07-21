using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Entities.Abstarct;

namespace Core.Database.Interfaces
{
    public interface ICrudBase<T> where T : class, IEntity, new()
    {
        T Add(T entity);
        void Delete(T entity);
        T Update(T entity);
        List<T> GetAll(Expression<Func<T, bool>> filter = null);
        T Get(Expression<Func<T, bool>> filter);
    }
}