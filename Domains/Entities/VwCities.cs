using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains.Entities
{
    public class VwCities
    {
        public Guid Id { get; set; }
      
        public string? CityAName { get; set; }

        public string? CityEName { get; set; }

        public Guid CountryId { get; set; }
        public string? CountryAName { get; set; }

        public string? CountryEName { get; set; }
        public Guid? UpdatedBy { get; set; }

        public int CurrentState { get; set; }

        public DateTime CreatedDate { get; set; }

        public Guid CreatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }
    }
}
