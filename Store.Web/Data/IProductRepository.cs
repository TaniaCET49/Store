namespace Store.Web.Data
{
    using System.Linq;
    using Store.Web.Data.Entities;
    
    public interface IProductRepository : IGenericRepository<Product>
    {
        IQueryable GetAllWithUsers();
    }
}
