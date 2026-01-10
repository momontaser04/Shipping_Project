using AutoMapper;
using BL.Contract;
using BL.Dtos;
using DAL.Contract;
using Domains.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Service
{
    public class RefreshTokenService : BaseService<TbRefreshTokens, RefreshTokenDto>,IRefreshToken
    {
        IGenericRepository<TbRefreshTokens> _repo;
        IMapper _mapper;
        public RefreshTokenService(IGenericRepository<TbRefreshTokens> repo, IMapper mapper,
            IUserService userService) : base(repo, mapper, userService)
        {
            _repo = repo;
            _mapper = mapper;
        }


 

        public async Task<bool> Refresh(RefreshTokenDto tokenDto)
        {
            var tokenList = await _repo.GetList(a => a.UserId == tokenDto.UserId && a.CurrentState == 1);
            foreach (var dbToken in tokenList)
            {
               await _repo.Changestatus(dbToken.Id, Guid.Parse(tokenDto.UserId), 2);
            }

            var dbTokens = _mapper.Map<RefreshTokenDto, TbRefreshTokens>(tokenDto);
           await _repo.AddAsync(dbTokens);
            return true;
        }


    }
}
