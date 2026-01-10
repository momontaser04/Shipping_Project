using AutoMapper;
using BL.Contract;
using BL.Dtos;
using BL.Service;
using DAL.Contract;
using Domains;
using Domains.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public class UserSenderService : BaseService<TbUserSenders, UserSenderDto>,IUserSender
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserSenderService(IGenericRepository<TbUserSenders> repo,IMapper mapper,
             IUserService userService, IUnitOfWork unitOfWork) : base(unitOfWork, mapper, userService)
        {
            _unitOfWork = unitOfWork;
        }
    }
}
