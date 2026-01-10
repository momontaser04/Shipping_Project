using System;
using System.Collections.Generic;

namespace Domains.Entities;

public partial class TbCarriers:BaseTable
{


    public string CarrierName { get; set; } = null!;

    public virtual ICollection<TbShippments> TbShipments { get; set; } = new List<TbShippments>();


}
