using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
