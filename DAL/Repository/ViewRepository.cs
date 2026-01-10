using DAL.Contract;
using DAL.DbContext_;
using DAL.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class ViewRepository<T> : IViewRepository<T> where T : class
    {
        private readonly ShippingContext _context;
        private readonly DbSet<T> _dbSet;
        private readonly ILogger<ViewRepository<T>> _logger;
        public ViewRepository(ShippingContext context, ILogger<ViewRepository<T>> log)
        {
            _context = context;
            _dbSet = _context.Set<T>();
            _logger = log;
        }
        public List<T> GetAll()
        {
            try
            {
                return _dbSet.AsNoTracking().ToList();
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex, "", _logger);
            }
        }

        public T GetById(Guid id)
        {
            try
            {
                return _dbSet.AsNoTracking().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex, "", _logger);
            }
        }
    }



}


