using System;
using System.Collections.Generic;

namespace Domains.Entities;

public partial class TbPaymentMethods:BaseTable
{


    public string? MethdAName { get; set; }

    public string? MethodEName { get; set; }

    public double? Commission { get; set; }

  

    public virtual ICollection<TbShippments> TbShippments { get; set; } = new List<TbShippments>();
}
