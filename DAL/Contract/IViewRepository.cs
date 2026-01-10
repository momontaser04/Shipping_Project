using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Contract
{
    public interface IViewRepository<T> where T : class
    {
        List<T> GetAll();
        T GetById(Guid id);
    }
}
