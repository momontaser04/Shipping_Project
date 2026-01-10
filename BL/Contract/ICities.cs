using BL.Dtos;
using Domains.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Contract
{
    public interface ICities:IBaseService<TbCities, CityDto>
    {

        List<CityDto>GetAllCities();
        List<CityDto> GetByCountry(Guid countryId);
    }
}
