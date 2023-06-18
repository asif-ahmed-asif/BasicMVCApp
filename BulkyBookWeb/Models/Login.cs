using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BulkyBookWeb.Models
{
    public class Login
    {
        [Required]
        [DisplayName("Email")]
        public string LEmail { get; set; }
        [Required]
        [DisplayName("Password")]
        [DataType(DataType.Password)]
        public string LPassword { get; set; }    
    }
}
