using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BulkyBookWeb.Models
{
    public class User
    {
        [Required]
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Enter a valid Email address!")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters!")]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Confirm Password")]
        [Compare("Password", ErrorMessage = "Password and Confirm Password does not match!")]
        [NotMapped]
        public string ConfirmPassword { get; set; }
    }
}
