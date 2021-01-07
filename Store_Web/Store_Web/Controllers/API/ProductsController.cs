using Microsoft.AspNetCore.Mvc;
using Store_Web.Data;

namespace Store_Web.Controllers.API
{
    [Route("api/[Controller]")]
    public class ProductsController : Controller
    {

        private readonly IProductRepository productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            return Ok(this.productRepository.GetAllWithUsers());
        }
    }
}
