using System;
using System.Collections.Generic;

namespace Domains.Entities;

public partial class TbCountries:BaseTable
{
   

    public string? CountryAName { get; set; }

    public string? CountryEName { get; set; }


    public virtual ICollection<TbCities> TbCities { get; set; } = new List<TbCities>();
}
