using System;
using System.Collections.Generic;

namespace Domains.Entities;

public partial class TbCities:BaseTable
{


    public string? CityAName { get; set; }

    public string? CityEName { get; set; }

    public Guid CountryId { get; set; }

 

    public virtual TbCountries Country { get; set; } = null!;

    public virtual ICollection<TbUserReceivers> TbUserReceivers { get; set; } = new List<TbUserReceivers>();

    public virtual ICollection<TbUserSenders> TbUserSenders { get; set; } = new List<TbUserSenders>();
}
