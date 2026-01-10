using AutoMapper;
using BL.Contract;
using BL.Dtos;
using BL.Service;
using DAL.Contract;
using Domains;
using Domains.Entities;

using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public class UserReceiverService : BaseService<TbUserReceivers, UserReceiverDto>,IUserReceiver
    {
   private readonly IUnitOfWork _unitOfWork;
        public UserReceiverService(IGenericRepository<TbUserReceivers> repo,IMapper mapper,
             IUserService userService,IUnitOfWork unitOfWork) : base( unitOfWork,mapper, userService)
        {
            _unitOfWork = unitOfWork;
        }
    }
}
