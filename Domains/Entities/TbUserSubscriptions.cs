using System;
using System.Collections.Generic;

namespace Domains.Entities;

public partial class TbUserSubscriptions:BaseTable
{


    public Guid UserId { get; set; }

    public Guid PackageId { get; set; }

    public DateTime SubscriptionDate { get; set; }

 

    public virtual TbSubscriptionPackages Package { get; set; } = null!;
}
