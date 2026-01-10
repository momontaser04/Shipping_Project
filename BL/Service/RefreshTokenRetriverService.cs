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
    public class RefreshTokenRetriverService : IRefreshTokenRetriver
    {
        IGenericRepository<TbRefreshTokens> _repo;
        IMapper _mapper;
        public RefreshTokenRetriverService(IGenericRepository<TbRefreshTokens> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public RefreshTokenDto GetByToken(string token)
        {
            var refreshToken = _repo.GetFirstOrDefault(a => a.Token == token);
            return _mapper.Map<TbRefreshTokens, RefreshTokenDto>(refreshToken);
        }
    }
}
