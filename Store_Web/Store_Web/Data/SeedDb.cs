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


        
        private readonly Random random;

        public SeedDb(DataContext context,IUserHelper userHelper)
        {
            this.context = context;
            this.userHelper = userHelper;
            this.random = new Random();
        }

        
        public async Task SeedAsync()
        {
            await this.context.Database.EnsureCreatedAsync();


            var user = await this.userHelper.GetUserByEmailAsync("tania.guerreiro.santos@formandos.cinel.pt");

            if (user == null)
            {

                user = new User
                {
                    FristName = " Tania ",
                    LastName = " Santoss ",
                    Email = "tania.guerreiro.santos@formandos.cinel.pt",
                    UserName = "Tigs",
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
                this.AddProduct("Pantufas Oficiais SLB", user);
                this.AddProduct("Águia Pequena Oficial SLB", user);
                await this.context.SaveChangesAsync();
            }

        }

        
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
