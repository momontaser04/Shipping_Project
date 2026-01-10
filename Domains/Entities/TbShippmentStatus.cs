using System;
using System.Collections.Generic;

namespace Domains.Entities;

public partial class TbShippmentStatus:BaseTable
{


    public Guid? ShippmentId { get; set; }

    public int CurrentState { get; set; }

    public string? Notes { get; set; }



    public virtual TbShippments? Shippment { get; set; }
}
