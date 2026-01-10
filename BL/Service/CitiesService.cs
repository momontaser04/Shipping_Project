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
    public class CitiesService:BaseService<TbCities,CityDto>, ICities
    {
        private readonly IViewRepository<VwCities> _reboCities;
        private readonly IGenericRepository<TbCities> _rebo;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        public CitiesService(IGenericRepository<TbCities> rebo, IMapper mapper,IUserService userService, IViewRepository<VwCities> reboCities) : base(rebo, mapper, userService)
        {
            _rebo = rebo;
            _mapper = mapper;
            _userService = userService;
            _reboCities = reboCities;
        }

        public List<CityDto> GetAllCities()
        {
          var cities = _reboCities.GetAll().Where(a=>a.CurrentState==1).ToList();
            var cityDtos = _mapper.Map<List<VwCities>,List<CityDto>>(cities);
            return cityDtos;
        }
        public List<CityDto> GetByCountry(Guid countryId)
        {
            var cities = _reboCities.GetAll().Where(a => a.CurrentState == 1 && a.CountryId==countryId).ToList();
            var cityDtos = _mapper.Map<List<VwCities>, List<CityDto>>(cities);
            return cityDtos;
        }

    }
}
