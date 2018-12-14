using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportStore.Models
{
    public interface IProductRepository
    {
        IQueryable<Product> products { get; }
        void SaveProduct(Product product);
        Product DeleteProduct(int productID);
    }
}
