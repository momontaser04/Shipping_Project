using BL.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Contract
{
    public interface IRefreshTokenRetriver
    {
        public RefreshTokenDto GetByToken(string token);
    }
}
