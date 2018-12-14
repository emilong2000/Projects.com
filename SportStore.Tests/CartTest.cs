using SportStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace SportStore.Tests
{
    public class CartTest
    {
        [Fact]
        public void Can_Add_New_Lines()
        {
            //Arrange-Create some test products
            Product P1 = new Product { ProductID = 1, Name = "P1" };
            Product P2 = new Product { ProductID = 2, Name = "P2" };

            //Arrange - Create a new cart
            Cart target = new Cart();

            //Act
            target.AddItem(P1, 1);
            target.AddItem(P2, 1);
            CartLine[] results = target.lines.ToArray();

            //Assert
            Assert.Equal(2, results.Length);
            Assert.Equal(P1, results[0].Product);
            Assert.Equal(P2, results[1].Product);
        }
        [Fact]
        public void Can_Add_Quantity_For_Existing_Lines()
        {
            //Arrange- Create some test products
            Product p1 = new Product { ProductID = 1, Name = "p1" };
            Product p2 = new Product { ProductID = 2, Name = "p2" };

            //Arrange - Craete  a new cart
            Cart target = new Cart();
            //Act
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            target.AddItem(p1, 10);
            CartLine[] results = target.lines.OrderBy(p => p.Product).ToArray();

            //Assert
            Assert.Equal(2, results.Length);
            Assert.Equal(11, results[0].Quantity);
            Assert.Equal(1, results[1].Quantity);

        }
        [Fact]
        public void Can_Remove_Line()
        {
            //Arrange- Create a new cart
            Product p1 = new Product { ProductID = 1, Name = "p1" };
            Product p2 = new Product { ProductID = 1, Name = "p2" };
            Product p3 = new Product { ProductID = 1, Name = "p3" };

            //Arrange - create a new cart
            Cart target = new Cart();
            //Arrange - Add some products to the Cart
            target.AddItem(p1, 1);
            target.AddItem(p2, 3);
            target.AddItem(p3, 5);
            target.AddItem(p2, 1);

            //Act
            target.RemoveLine(p2);
            //Assert
            Assert.Equal(0, target.lines.Where(c => c.Product == p2).Count());
            Assert.Equal(2, target.lines.Count());
        }
        [Fact]
        public void Calculate_Cart_Total()
        {
            //Arrange - craete some test products
            Product p1 = new Product { ProductID = 1, Name = "p1", Price=100m };
            Product p2 = new Product { ProductID = 1, Name = "p1", Price=50m };

            //Arrange - Create a new cart
            Cart target = new Cart();

            //Act
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            target.AddItem(p1, 3);
            decimal result = target.ComputeTotalValue();

            //Assert
            Assert.Equal(450m, result);
        }
    }
}
