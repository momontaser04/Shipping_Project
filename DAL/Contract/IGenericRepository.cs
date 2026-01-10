using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Contract
{
    public interface IGenericRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<T?> GetByIdAsync(Guid id);
        Task<bool> AddAsync(T entity);
        bool Add(T entity, out Guid id);
        Task<bool> UpdateAsync(T entity);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> Changestatus(Guid id,Guid userid, int status=1);
        T GetFirstOrDefault(Expression<Func<T, bool>> filter);
       Task< List<T>> GetList(Expression<Func<T, bool>> filter);
        Task<List<TResult>> GetList<TResult>(
        Expression<Func<T, bool>>? filter = null,
        Expression<Func<T, TResult>>? selector = null,
        Expression<Func<T, object>>? orderBy = null,
        bool isDescending = false,
        params Expression<Func<T, object>>[] includers);

        Task<PagedResult<TResult>> GetPagedList<TResult>(
     int pageNumber,
     int pageSize,
     Expression<Func<T, bool>>? filter = null,
     Expression<Func<T, TResult>>? selector = null,
     Expression<Func<T, object>>? orderBy = null,
     bool isDescending = false,
     params Expression<Func<T, object>>[] includers);



    }
 
}
