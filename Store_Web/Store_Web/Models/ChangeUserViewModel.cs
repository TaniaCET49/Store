using System.ComponentModel.DataAnnotations;

namespace Store_Web.Models
{
    public class ChangeUserViewModel
    {
        [Required, Display(Name = "Frist Name")]
        public string FristName { get; set; }

        [Required, Display(Name = "Last Name")]
        public string LastName { get; set; }
    }
}
