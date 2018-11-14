using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Xgame.Core.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAll();

        Task<List<T>> Find(Expression<Func<T, bool>> predicate);

        Task<T> GetById(int Id);

        Task Create(T entity);

        Task Update(T entity);

        Task Delete(T entity);
    }
}
