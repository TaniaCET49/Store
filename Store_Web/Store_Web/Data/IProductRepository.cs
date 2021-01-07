using Store_Web.Data.Enteties;
using System.Linq;

namespace Store_Web.Data
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        IQueryable GetAllWithUsers();
    }
}
