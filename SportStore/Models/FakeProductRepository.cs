using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportStore.Models
{
    public class FakeProductRepository /*: IProductRepository */
    {
        public IQueryable<Product> products => new List<Product>
        {
            new Product{Name ="Football", Price=25},
            new Product{Name ="Surf board", Price = 179},
            new Product{Name ="Running Shoes",Price = 95}
        }.AsQueryable<Product>();
    }
}
