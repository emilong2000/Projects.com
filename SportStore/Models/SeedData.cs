using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportStore.Models
{
    public class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            ApplicationDbContext context = app.ApplicationServices
                .GetRequiredService<ApplicationDbContext>();
            context.Database.Migrate();
            if (!context.Products.Any())
            {
                context.Products.AddRange(
                    new Product { Name = "Kayak", Description = "A Boat for one person", Category = "Watersports", Price = 275 },
                    new Product { Name = "LifeJacket", Description = "Protective and fashionable", Category = "Watersports", Price = 48.9m },
                    new Product { Name = "Soccer ball", Description = "Fifa approved size and weight", Category = "Soccer", Price = 19.50m },
                    new Product { Name = "Corner flags", Description = "Give your playing field a professional touch", Category = "Soccer", Price = 34.95m },
                    new Product { Name = "Stadium", Description = "Flat-packed 35000-seat stadium", Category = "Soccer", Price = 97500 },
                    new Product { Name = "Thinking cap", Description = "Improve brain efficiency by 75%", Category = "Chess", Price = 16 },
                    new Product { Name = "Unsteady Chair", Description = "Secretly give your opponent a disappointment", Category = "Chess", Price = 29.95m },
                    new Product { Name = "Human chess board", Description = "A fun game for the family", Category = "Chess", Price = 75 },
                    new Product { Name = "Bling-Bling king", Description = "Gold-platted, diamond-sudded king", Category = "Chess", Price = 1200 });
                context.SaveChanges();
            }
        }
    }
}
