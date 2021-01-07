using System;
using System.ComponentModel.DataAnnotations;

namespace Store_Web.Data.Enteties
{
    public class Product : IEntity
    {

        public int Id { get; set; }

        [MaxLength(50, ErrorMessage = "The field {0} only can contain {1} characters lenght.")]
        [Required]
        [Display(Name = "Produto")]
        public string Name { get; set; }



        [Required]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public decimal Price { get; set; }




        [Display(Name = "Image")]
        public string ImageUrl { get; set; }





        [Display(Name = "Last Purchase")]
        public DateTime? LastPurchase { get; set; }



        [Display(Name = "Last Sale")]
        public DateTime? LastSale { get; set; }


        [Display(Name = "Is Available ?")]
        public bool IsAvailable { get; set; }


        [Required]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = false)]
        public double Stock { get; set; }



        public User User { get; set; }


        


        public string ImageFullPath
        {
            get
            {
                if (string.IsNullOrEmpty(this.ImageUrl))
                {
                    return null;
                }
                return $"https://localhost:44397/Products {this.ImageUrl.Substring(1)}";
            }
        }

    }
}
