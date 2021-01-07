using Microsoft.AspNetCore.Http;
using Store_Web.Data.Enteties;
using System.ComponentModel.DataAnnotations;

namespace Store_Web.Models
{
    public class ProductsViewModel : Product
    {
        [Display(Name = "Image")]
        public IFormFile ImageFile { get; set; }
    }
}
