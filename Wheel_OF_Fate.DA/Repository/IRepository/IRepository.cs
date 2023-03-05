using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Wheel_OF_Fate.DA.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task Create(T entity);
        Task Remove(T entity);
        Task Save();
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null);
        Task<T> Get(Expression<Func<T, bool>>? filter = null, bool tracked = true);
    }
}
