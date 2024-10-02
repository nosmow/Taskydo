using System.ComponentModel.DataAnnotations;

namespace Taskydo.Models
{
    public class RegistrationViewModel
    {
        [Required(ErrorMessage = "The field {0} is required")]
        [EmailAddress(ErrorMessage = "The field must be a valid email address")]
        public string email { get; set; }
        [Required(ErrorMessage = "The field {0} is required")]
        [DataType(DataType.Password)]
        public string password { get; set; }
    }
}
