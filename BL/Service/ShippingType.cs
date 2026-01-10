using AutoMapper;
using BL.Contract;
using BL.Dtos;
using DAL.Contract;
using Domains.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Service
{
    public class ShippingType : BaseService<TbShippingTypes, ShippingTypeDto>,IShippingType
    {
        private readonly IGenericRepository<TbShippingTypes> _rebo;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        public ShippingType(IGenericRepository<TbShippingTypes> rebo,IMapper mapper,IUserService userService): base(rebo,mapper, userService)
        {
            _rebo = rebo;
            _mapper = mapper;
            _userService = userService;
        }
       


    }
}
