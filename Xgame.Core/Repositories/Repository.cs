using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xgame.Db;

namespace Xgame.Core.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly XgameContext _context;

        public Repository(XgameContext context)
        {
            _context = context;
        }

        protected async Task Save() => await _context.SaveChangesAsync();
       

        public async Task Create(T entity)
        {
            await _context.AddAsync(entity);
            await Save();
        }

        public async Task Delete(T entity)
        {
            _context.Remove(entity);
            await Save();
        }

        public async Task<List<T>> Find(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(predicate).ToListAsync();
        }

        public async Task<List<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetById(int Id)
        {
            return await _context.Set<T>().FindAsync(Id);
        }

        public async Task Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await Save();
        }
    }
}
