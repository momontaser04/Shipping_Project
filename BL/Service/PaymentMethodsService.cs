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
    public class PaymentMethodsService : BaseService<TbPaymentMethods,PaymentMethodDto>,IPaymentMethods
    {
        public PaymentMethodsService(IGenericRepository<TbPaymentMethods> repo,IMapper mapper,
             IUserService userService) : base(repo,mapper, userService)
        {

        }
    }
}
