using System;
using System.Collections.Generic;

namespace BL.Dtos;

public partial class SettingDto : BaseDto
{
    public double? KiloMeterRate { get; set; }

    public double? KilooGramRate { get; set; }
}
