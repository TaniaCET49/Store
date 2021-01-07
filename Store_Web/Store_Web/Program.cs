using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Store_Web.Data;

namespace Store_Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            /* desconstrução do inicio da interação com o  site e a base de dados para poderemos criar os dados random caso esteja a Db vazia por "norma é sempre o mesmo passo" */
            var host = CreateWebHostBuilder(args).Build();
            RunSeeding(host);
            host.Run();
        }


        private static void RunSeeding(IWebHost host)
        {
            /*função para criação dos dados */
            var scopeFactory = host.Services.GetService<IServiceScopeFactory>();
            using (var scope = scopeFactory.CreateScope())
            {
                var seeder = scope.ServiceProvider.GetService<SeedDb>();
                seeder.SeedAsync().Wait();
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
