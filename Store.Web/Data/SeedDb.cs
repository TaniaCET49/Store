namespace Store.Web.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Entities;
    using Store.Web.Helpers;

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

            await this.userHelper.CheckRoleAsync("Admin");
            await this.userHelper.CheckRoleAsync("Customer");

            var user = await this.userHelper.GetUserByEmailAsync("rafaelsantos@portugalmail.pt");
            if (user == null)
            {
                user = new User
                {
                    FirstName = "Rafael",
                    LastName = "Santos",
                    Email = "rafaelsantos@portugalmail.pt",
                    UserName = "rafaelsantos@portugalmail.pt",
                    PhoneNumber = "212345456"

                };

                var result = await this.userHelper.AddUserAsync(user, "123456");
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create the user in seeder");
                }

                await this.userHelper.AddUserToRoleAsync(user, "Admin");
            }

            var isRole = await this.userHelper.IsUserInRoleAsync(user, "Admin");

            if (!isRole)
            {
                await this.userHelper.AddUserToRoleAsync(user, "Admin");
            }

            if (!this.context.Products.Any())
            {
                this.AddProduct("Equipamento oficial SLB", user);
                this.AddProduct("Chuteiras oficiais", user);
                this.AddProduct("Águia pequena", user);
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
