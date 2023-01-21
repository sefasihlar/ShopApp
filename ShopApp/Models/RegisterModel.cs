using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace ShopApp.WebUI.Models
{
    public class RegisterModel
    {
        [Required]
        public string FullName { get; set; }

        [Required]
        public string UserName { get; set; }
        [Required]

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password")]
        [DataType(DataType.Password)]
        [Required]
        public string RePassword { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required]
        public string Email { get; set; }


    }
}
