using AutoMapper;
using BL.Contract;
using BL.Contract.Shipment;
using BL.Dtos;
using DAL.Contract;
using Domains.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Service.Shipment
{
    
    public class ShipmentStatusService : BaseService<TbShippmentStatus, ShippmentStatusDto>, IShipmentStatus
    {
        IUnitOfWork _uow;
        IUserService _userService;
        IGenericRepository<TbShippmentStatus> _repo;
        IMapper _mapper;
        public ShipmentStatusService(IGenericRepository<TbShippmentStatus> repo, IMapper mapper,
             IUserService userService, IUnitOfWork uow) : base(uow, mapper, userService)
        {
            _uow = uow;
            _repo = repo;
            _mapper = mapper;
            _userService = userService;
        }

    }
}
