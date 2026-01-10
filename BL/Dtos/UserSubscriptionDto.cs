using System;
using System.Collections.Generic;

namespace BL.Dtos;

public partial class UserSubscriptionDto : BaseDto
{
    public Guid UserId { get; set; }

    public Guid PackageId { get; set; }

    public DateTime SubscriptionDate { get; set; }
}
