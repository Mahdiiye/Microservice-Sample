using Microsoft.EntityFrameworkCore;
using PlatformService.Models;
using System.Diagnostics.CodeAnalysis;

namespace PlatformService.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder app,bool isProd)
        {
            
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var arg = serviceScope.ServiceProvider.GetService<AppDbContext>();
                if (arg != null)
                {
                    seedData(arg , isProd);
                }
            }
        }

        
        private static void seedData(AppDbContext context , bool isProd)
        {
            if (isProd)
            {
                Console.WriteLine("--> Attempting to apply migrations...");
                try
                {
                    context.Database.Migrate();
                }
                catch (Exception ex)
                {

                    Console.Error.WriteLine(ex.ToString());
                    Console.WriteLine($"--> Could not run migrations: {ex.Message}");
                }
                
            }

            if (!context.Platforms.Any())
            {
                Console.WriteLine("--Seeding Data...");
                context.Platforms.AddRange(
                    new Platform() { Name = "DotNet", Publisher = "Microsoft", Cost = "Free" },
                    new Platform() { Name = "SQL Server", Publisher = "Microsoft", Cost = "Free" },
                    new Platform() { Name = "Kubernetes", Publisher = "Cloud Native", Cost = "Free" }
                );
                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("--we already have data");
            }
        }
    }
}
