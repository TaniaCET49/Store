using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Store_Web.Data.Enteties;

namespace Store_Web.Data
{
    public class DataContext : IdentityDbContext<User>
    {


        public DbSet<Product> Products { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }


    }
}
