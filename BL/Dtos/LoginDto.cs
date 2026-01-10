using AppResources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Dtos
{
    public class LoginDto
    {

        [Required(ErrorMessageResourceType = typeof(AppResources.Shipping), ErrorMessageResourceName = "EmailRequired")]
        [EmailAddress(ErrorMessageResourceType = typeof(AppResources.Shipping), ErrorMessageResourceName = "InvalidEmail")]
        public string Email { get; set; }

        [Required(ErrorMessageResourceType = typeof(AppResources.Shipping), ErrorMessageResourceName = "PasswordRequired")]
        [MinLength(6, ErrorMessageResourceType = typeof(AppResources.Shipping), ErrorMessageResourceName = "PasswordMinLength")]
        public string Password { get; set; }
    }
}
