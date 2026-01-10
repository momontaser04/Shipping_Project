using System;
using System.Collections.Generic;

namespace Domains.Entities;

public partial class TbShippingTypes:BaseTable
{


    public string? ShippingTypeAName { get; set; }

    public string? ShippingTypeEName { get; set; }

    public double ShippingFactor { get; set; }



    public virtual ICollection<TbShippments> TbShippments { get; set; } = new List<TbShippments>();
}
