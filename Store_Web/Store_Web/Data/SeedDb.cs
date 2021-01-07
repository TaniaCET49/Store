using Microsoft.AspNetCore.Identity;
using Store_Web.Data.Enteties;
using Store_Web.Helpers;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Store_Web.Data
{
    public class SeedDb
    {
        private readonly DataContext context;
        private readonly IUserHelper userHelper;


        /*gerador de 1ªs dados */
        private Random random;

        public SeedDb(DataContext context,IUserHelper userHelper)
        {
            this.context = context;
            this.userHelper = userHelper;
            this.random = new Random();
        }

        /* Processo de criação de dados na primeira vez que a base de dados é utilizada  */
        public async Task SeedAsync()
        {
            await this.context.Database.EnsureCreatedAsync();


            var user = await this.userHelper.GetUserByEmailAsync("Xtare16.soares@gmail.com");

            if (user == null)
            {

                user = new User
                {
                    FristName = " Tiago ",
                    LastName = " Soares ",
                    Email = "Xtare16.soares@gmail.com",
                    UserName = "XtareS",
                    PhoneNumber = "*********"
                };

                var result = await this.userHelper.AddUserAsync(user, "123456");

                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could Not Create the User in Seeder");
                }

            }





            if (!this.context.Products.Any())
            {
                this.AddProduct("Equipamento Oficial SLB", user);
                this.AddProduct("Chuteiras Oficiais SLB", user);
                this.AddProduct("Águia Pequena Oficial SLB", user);
                await this.context.SaveChangesAsync();
            }

        }

        /* dados que serão colocados automaticamente na primeira vez que a base de dados é utilizada  */
        private void AddProduct(string name, User user)
        {
            this.context.Products.Add(new Product
            {
                Name = name,
                Price = this.random.Next(200),
                IsAvailable = true,
                Stock = this.random.Next(100),
                User = user
            });
        }
    }
}
