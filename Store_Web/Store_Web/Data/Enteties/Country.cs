using System.ComponentModel.DataAnnotations;

namespace Store_Web.Data.Enteties
{
    public class Country : IEntity
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "País")]
        public string Name { get; set; }
    }
}
