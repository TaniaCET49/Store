using System.ComponentModel.DataAnnotations;

namespace Store_Web.Models
{
    public class ChangePasswordViewModel
    {

        [Required, Display(Name = "Current Password")]
        public string OldPassword { get; set; }

        [Required, Display(Name = "New Password")]
        public string NewPassword { get; set; }

        [Required, Compare("NewPassowrd")]
        public string Confirm { get; set; }

    }
}
