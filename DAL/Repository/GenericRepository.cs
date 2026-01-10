using DAL.Contract;
using DAL.DbContext_;
using DAL.Exceptions;
using DAL.Models;
using Domains.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseTable
    {
        private readonly ShippingContext _context;
        private readonly DbSet<T> _dbSet;
        private readonly ILogger<GenericRepository<T>> _logger;
        public GenericRepository(ShippingContext context, ILogger<GenericRepository<T>> logger)
        {
            _context = context;
            _dbSet = _context.Set<T>();
            _logger = logger;
        }


        public async Task<bool> AddAsync(T entity)
        {
            try
            {
                entity.CreatedDate = DateTime.Now;
                entity.CurrentState = 1;
                await _dbSet.AddAsync(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
              throw new DataAccessException(ex, "Error adding record", _logger);
            }
        }

        public async Task<bool> Changestatus(Guid id,Guid userid, int status = 1)
        {
            try
            {
                var entity = await GetByIdAsync(id);

                if (entity != null)
                {
                    entity.CurrentState = status;
                    entity.UpdatedBy = userid;
                    entity.UpdatedDate = DateTime.Now;
                    await _context.SaveChangesAsync();
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {

               throw new DataAccessException(ex, "Error changing status", _logger);
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            try { 
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            return false; }
        catch (Exception ex)
            {
                
                throw new DataAccessException(ex, "Error deleting record", _logger);
            }
        }


        public async Task<List<T>> GetAllAsync()
        {
            try
            {
                return await _dbSet.Where(a=>a.CurrentState>0).ToListAsync();
            }
            catch (Exception ex)
            {
               throw new DataAccessException(ex, "Error fetching all records",_logger);
            }
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            try
            {
                return await _dbSet.FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex, "Error fetching record by ID", _logger);
            }

        }
        public async Task<bool> UpdateAsync(T entity)
        {
            try
            {
                var dbdata = await _dbSet.FindAsync(entity.Id);
                if (dbdata == null)
                    return false;

                // نحافظ على بيانات الإنشاء
                entity.CreatedDate = dbdata.CreatedDate;
                entity.CreatedBy = dbdata.CreatedBy;
                entity.CurrentState = 1; 

                // تحديث القيم
                _context.Entry(dbdata).CurrentValues.SetValues(entity);
                dbdata.UpdatedDate = DateTime.Now;

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex, "Error updating record", _logger);
            }
        }
        public T GetFirstOrDefault(Expression<Func<T, bool>> filter)
        {
            try
            {
                return _dbSet.Where(filter).AsNoTracking().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex, "", _logger);
            }
        }

        // Method to get a list of records based on a filter
        public async Task<List<T>> GetList(Expression<Func<T, bool>> filter)
        {
            try
            {
                return await  _dbSet.Where(filter).AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex, "", _logger);
            }
        }

        public  bool Add(T entity, out Guid id)
        {
            try
            {
                entity.CreatedDate = DateTime.Now;
                 _dbSet.Add(entity);
                _context.SaveChanges();
                id = entity.Id;
                return  true;
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex, "", _logger);
            }
        }

        public async Task<List<TResult>> GetList<TResult>(
              Expression<Func<T, bool>>? filter = null,
              Expression<Func<T, TResult>>? selector = null,
              Expression<Func<T, object>>? orderBy = null,
              bool isDescending = false,
              params Expression<Func<T, object>>[] includers)
        {
            try
            {
                IQueryable<T> query = _dbSet.AsQueryable();

                // Apply includes
                foreach (var include in includers)
                    query = query.Include(include);

                // Apply filter
                if (filter != null)
                    query = query.Where(filter);

                // Apply ordering
                if (orderBy != null)
                    query = isDescending
                        ? query.OrderByDescending(orderBy)
                        : query.OrderBy(orderBy);

                query = query.AsNoTracking();

                // Apply projection
                if (selector != null)
                    return await query.Select(selector).ToListAsync();

                return await query.Cast<TResult>().ToListAsync();
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex, "", _logger); // Or your custom exception
            }
        }


        public async Task<PagedResult<TResult>> GetPagedList<TResult>(
       int pageNumber,
       int pageSize,
       Expression<Func<T, bool>>? filter = null,
       Expression<Func<T, TResult>>? selector = null,
       Expression<Func<T, object>>? orderBy = null,
       bool isDescending = false,
       params Expression<Func<T, object>>[] includers)
        {
            try
            {
                IQueryable<T> query = _dbSet.AsQueryable();

                
                foreach (var include in includers)
                    query = query.Include(include);

                if (filter != null)
                    query = query.Where(filter);

                // Total count before pagination
                int totalCount = await query.CountAsync();

                // Apply ordering
                if (orderBy != null)
                {
                    query = isDescending
                        ? query.OrderByDescending(orderBy)
                        : query.OrderBy(orderBy);
                }

                query = query.AsNoTracking();

                // Apply paging
                query = query
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize);

                // Apply projection
                List<TResult> items;
                if (selector != null)
                    items = await query.Select(selector).ToListAsync();
                else
                    items = await query.Cast<TResult>().ToListAsync();

                // Calculate total pages
                int totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

                return new PagedResult<TResult>
                {
                    Items = items,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalCount = totalCount,
                    TotalPages = totalPages
                };
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex, "", _logger);
            }
        }
    }
}
