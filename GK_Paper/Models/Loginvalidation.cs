using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GK_Paper.Models
{
    public class Loginvalidation
    {
        //[Required(ErrorMessage = "Password is Required.")]
        //[RegularExpression(@"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$", ErrorMessage = "Invalid Password")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Inedx No no is Required")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Use Digits only please")]
        [StringLength(12, ErrorMessage = "Inedx No must be 8 digits", MinimumLength = 5)]
        public string Inedx_no { get; set; }
    }
}