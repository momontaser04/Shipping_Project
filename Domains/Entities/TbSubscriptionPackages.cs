using System;
using System.Collections.Generic;

namespace Domains.Entities;

public partial class TbSubscriptionPackages:BaseTable
{


    public string PackageName { get; set; } = null!;

    public int ShippimentCount { get; set; }

    public double NumberOfKiloMeters { get; set; }

    public double TotalWeight { get; set; }

 

    public virtual ICollection<TbUserSubscriptions> TbUserSubscriptions { get; set; } = new List<TbUserSubscriptions>();
}
