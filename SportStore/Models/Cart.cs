using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportStore.Models
{
    public class Cart
    {
        private List<CartLine> LineCollection = new List<CartLine>();

        public virtual void AddItem(Product product, int quantity)
        {
            CartLine line = LineCollection.Where(p =>p.Product.ProductID== product.ProductID).FirstOrDefault();

            if(line == null)
            {
                LineCollection.Add(new CartLine
                {
                    Product = product,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }
        public virtual void RemoveLine(Product product) => LineCollection.RemoveAll(p => p.Product.ProductID == product.ProductID);
        public virtual decimal ComputeTotalValue() => LineCollection.Sum(p => p.Product.Price * p.Quantity);
        public virtual void Clear() => LineCollection.Clear();
        public virtual IEnumerable<CartLine> lines => LineCollection;
    }
    public class CartLine
    {
        public int CartLineId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }

    }
}
