using AutoMapper;
using BL.Contract;
using BL.Mapping;
using DAL.Contract;
using Domains.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BL.Service
{
    public class BaseService<T, DTO> : IBaseService<T,DTO> where T : BaseTable
    {
        private readonly IGenericRepository<T> _repository;

        private readonly AutoMapper.IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IUnitOfWork _unitOfWork;
        public BaseService(IGenericRepository<T> repository,AutoMapper.IMapper mapper, IUserService userService)
        {
            _repository = repository;
            _mapper = mapper;
            _userService = userService;
        }
        public BaseService(IUnitOfWork unitOfWork, AutoMapper.IMapper mapper, IUserService userService)
        {
            _unitOfWork = unitOfWork;
            _repository = _unitOfWork.Repository<T>();
            _mapper = mapper;
            _userService = userService;

        }

        public bool Add(DTO entity, out Guid id)
        {
            var dbObject = _mapper.Map<DTO, T>(entity);
            dbObject.CreatedBy = _userService.GetLoggedInUser();
            dbObject.CurrentState = 1;
            return _repository.Add(dbObject, out id);
        }
        public async Task<bool> AddAsync(DTO entity)
        {
            var result = _mapper.Map<DTO,T>(entity);
            result.CreatedBy = _userService.GetLoggedInUser();
            return await _repository.AddAsync(result);
        }

        public async Task<bool> Changestatus(Guid id, int status = 1)
        {
       return await _repository.Changestatus(id, _userService.GetLoggedInUser(),status);
        }

        public async Task<List<DTO>> GetAllAsync()
        {
            var result = await _repository.GetAllAsync();
            return _mapper.Map<List<T>,List<DTO>>(result);
        }

        public async Task<DTO?> GetByIdAsync(Guid id)
        {
            var result = await _repository.GetByIdAsync(id);
          
            return _mapper.Map<T, DTO>(result!);
        }

        public  async Task<bool> UpdateAsync(DTO entity)
        {
            var result = _mapper.Map<DTO,T>(entity);

            result.UpdatedBy = _userService.GetLoggedInUser() ;
           
            return await _repository.UpdateAsync(result) ;
        }


    }
}
