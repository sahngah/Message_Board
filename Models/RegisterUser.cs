using System.ComponentModel.DataAnnotations;
using System;

namespace userDashboard.Models
{
    public class RegisterUser : BaseEntity
    {
        [Required]
        [MinLength(2)]
        [RegularExpression("^[a-zA-Z]+$")]
        public string Firstname { get; set; }
        [Required]
        [MinLength(2)]
        [RegularExpression("^[a-zA-Z]+$")]
        public string Lastname { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MinLength(8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and confirmation must match!!!")]
        public string Passwordconfirmation { get; set;}
    }
}