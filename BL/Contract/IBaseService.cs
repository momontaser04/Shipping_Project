using Domains.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Contract
{
    public interface IBaseService<T,DTO>
    {

        Task<List<DTO>> GetAllAsync();
        Task<DTO?> GetByIdAsync(Guid id);
        Task<bool> AddAsync(DTO entity);
        bool Add(DTO entity,out Guid id);
        Task<bool> UpdateAsync(DTO entity);
        Task<bool> Changestatus(Guid id,  int status = 1);
    }
}
