using BL.Dtos;
using Domains;
using Domains.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Contract
{
    public interface IUserReceiver : IBaseService<TbUserReceivers, UserReceiverDto>
    {

    }
}
