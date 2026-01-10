using System;
using System.Collections.Generic;

namespace BL.Dtos;

public partial class SubscriptionPackageDto : BaseDto
{
    public string PackageName { get; set; } = null!;

    public int ShippimentCount { get; set; }

    public double NumberOfKiloMeters { get; set; }

    public double TotalWeight { get; set; }
}
