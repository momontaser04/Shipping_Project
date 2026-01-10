using BL.Dtos;
using Domains;
using Domains.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;

namespace BL.Contract
{
    public interface IShipment : IBaseService<TbShippments,ShippmentDto>
    {
        public Task Create(ShippmentDto dto);
        public Task Edit(ShippmentDto dto);
        public Task<List<ShippmentDto>> GetShipments();

        public Task<PagedResult<ShippmentDto>> GetShipments(int pageNumber, int pageSize);

        public Task<ShippmentDto> GetShipment(Guid id);
    }
}
