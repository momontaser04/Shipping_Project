using System;
using System.Collections.Generic;

namespace BL.Dtos;

public partial class ShippmentStatusDto : BaseDto
{
    public Guid? ShippmentId { get; set; }

 

    public string? Notes { get; set; }

}
