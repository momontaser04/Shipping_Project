using AppResources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BL.Dtos;

public partial class CityDto : BaseDto
{
    [StringLength(100, MinimumLength = 3, ErrorMessageResourceName = "NameLenght", ErrorMessageResourceType = typeof(Messages))]
    public string? CityAname { get; set; }

    [Required(ErrorMessageResourceName = "NameEnRequired", ErrorMessageResourceType = typeof(Messages), AllowEmptyStrings = false)]
    [StringLength(100, MinimumLength = 3, ErrorMessageResourceName = "NameLenght", ErrorMessageResourceType = typeof(Messages))]
    public string? CityEname { get; set; }

  
    public string? CountryAname { get; set; }

    public string? CountryEname { get; set; }

    [Required(ErrorMessageResourceName = "CountryRequired", ErrorMessageResourceType = typeof(Messages), AllowEmptyStrings = false)]
    public Guid CountryId { get; set; }
}
