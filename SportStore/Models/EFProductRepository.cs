using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportStore.Models
{
    public class EFProductRepository : IProductRepository
    {
        private ApplicationDbContext context;
        public EFProductRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }
        public IQueryable<Product> products => context.Products;

        public Product DeleteProduct(int productID)
        {
            Product DbEntry = context.Products.FirstOrDefault(p => p.ProductID == productID);
            if(DbEntry != null)
            {
                context.Remove(DbEntry);
                context.SaveChanges();
            }
            return DbEntry;
        }
        public void SaveProduct(Product product)
        {
            if(product.ProductID == 0)
            {
                context.Products.Add(product);
            }
            else
            {
                Product DbEntry = context.Products.FirstOrDefault(m => m.ProductID == product.ProductID);
                if(DbEntry != null)
                {
                    DbEntry.Name = product.Name;
                    DbEntry.Description = product.Description;
                    DbEntry.Price = product.Price;
                    DbEntry.Category = product.Category;
                }
            }
            context.SaveChanges();
        }
        
    }
}
