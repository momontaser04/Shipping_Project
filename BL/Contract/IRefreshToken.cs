using BL.Dtos;
using Domains.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Contract
{
    public interface IRefreshToken:IBaseService<TbRefreshTokens,RefreshTokenDto>
    {

        public  Task<bool> Refresh(RefreshTokenDto tokenDto);
    }
}
