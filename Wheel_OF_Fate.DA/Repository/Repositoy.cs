using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Wheel_OF_Fate.DA.Data;
using Wheel_OF_Fate.DA.Repository.IRepository;

namespace Wheel_OF_Fate.DA.Repository
{
    public class Repositoy<T> : IRepository<T> where T: class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> _dbset;
        public Repositoy(ApplicationDbContext db)
        {
            _db = db;
            this._dbset = _db.Set<T>();
        }

        public async Task Create(T entity)
        {
            await _dbset.AddAsync(entity);
            await Save();
        }
        public async Task Update(T entity)
        {
            _dbset.Update(entity);
            await Save();
        }
        public async Task<T> Get(Expression<Func<T, bool>>? filter = null, bool tracked = false)
        {
            IQueryable<T> query = _dbset;
            if (tracked)
            {
                query = query.AsNoTracking();
            }
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null)
        {
            IQueryable<T> query = _dbset;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.ToListAsync();
        }

        public async Task Remove(T entity)
        {
            _dbset.Remove(entity);
            await Save();
        }

        public async Task Save()
        {
            await _db.SaveChangesAsync();
        }
    }
}
