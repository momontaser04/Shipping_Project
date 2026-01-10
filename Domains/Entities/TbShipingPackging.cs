using Domains.Entities;
using System;
using System.Collections.Generic;

namespace Domains;

public  class TbShipingPackging : BaseTable
{
    public string? ShipingPackgingAname { get; set; }

    public string? ShipingPackgingEname { get; set; }

    public virtual ICollection<TbShippments> TbShippments { get; set; } = new List<TbShippments>();
}
