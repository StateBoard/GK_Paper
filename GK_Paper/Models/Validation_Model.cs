using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GK_Paper.Models
{
    public class Validation_Model
    {


        [Required(ErrorMessage = " User Id is required")]
        [RegularExpression(@"^(\d{8})$", ErrorMessage = "Enter only 8 digit Number")]
        [StringLength(8, MinimumLength = 8, ErrorMessage = "Please fill the valid User Id")]
        public int ID { get; set; }

        [Required(ErrorMessage = "  Name  is required")]
        [DataType(DataType.Text)]
        [RegularExpression("^[a-zA-Z_ ]*$", ErrorMessage = "Please fill the valid name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email ID is Required.")]
        [RegularExpression(@"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$", ErrorMessage = "Invalid Email ID")]
        public string Email_ID { get; set; }

        [Required(ErrorMessage = "Mobile No is Required")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Use Digits only please")]
        [StringLength(12, ErrorMessage = "IP Adress No must be 10 digits", MinimumLength = 10)]
        public string Mobile_no { get; set; }

        //[Required(ErrorMessage = "Password is Required.")]
        //[RegularExpression(@"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$", ErrorMessage = "Invalid Password")]
        //[DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Inedx No no is Required")]
        [RegularExpression(@"^[0-8]+$", ErrorMessage = "Use Digits only please")]
        [StringLength(12, ErrorMessage = "Inedx No must be 7 digits", MinimumLength = 5)]
        public string Inedx_no { get; set; }

        public System.DateTime date { get; set; }

        [Required(ErrorMessage = "IP Adress no is Required")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Use Digits only please")]
        [StringLength(12, ErrorMessage = "Inedx No must be 8 digits", MinimumLength = 5)]
        public string IP_Adress { get; set; }

        [Required(ErrorMessage = "Aadhar no is Required")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Use Digits only please")]
        [StringLength(12, ErrorMessage = "Aadhar No must be 12 digits", MinimumLength = 12)]
        public string adhar_no { get; set; }
    }
}