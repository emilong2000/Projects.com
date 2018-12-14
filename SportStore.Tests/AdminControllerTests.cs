using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using SportStore.Controllers;
using SportStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace SportStore.Tests
{
    public class AdminControllerTests 
    {
        [Fact]
        public void Index_Contains_All_Products()
        {
            //Arrange- create the mock repository
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.products).Returns(new Product[] {
            new Product{ProductID = 1, Name = "p1" },
            new Product{ProductID = 2, Name = "p2" },
            new Product{ProductID = 3, Name = "p3" }
            }.AsQueryable<Product>());

            //Arrange - Create the controller
            AdminController target = new AdminController(mock.Object);
            //Action
            Product[] result = GetViewModel<IEnumerable<Product>>(target.Index())?.ToArray();

            //Assert
            Assert.Equal(3, result.Length);
            Assert.Equal("p1", result[0].Name);
            Assert.Equal("p2", result[1].Name);
            Assert.Equal("p3", result[2].Name);
        }
        private T GetViewModel<T>(IActionResult result) where T : class
        {
            return (result as ViewResult)?.ViewData.Model as T;
        }
        [Fact]
        public void Can_Edit_Product()
        {
            //Arrange - Create the Mock repository
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m=>m.products).Returns( new Product[]{
                new Product{ProductID = 1, Name = "p1"},
                new Product{ProductID = 2, Name = "p2"},
                new Product{ProductID = 3, Name = "p3"}
            }.AsQueryable<Product>());

            //Arrange - create the controller
            AdminController target = new AdminController(mock.Object);
            //Act
            Product p1 = GetViewModel<Product>(target.Edit(1));
            Product p2 = GetViewModel<Product>(target.Edit(2));
            Product p3 = GetViewModel<Product>(target.Edit(3));

            //Assert
            Assert.Equal(1, p1.ProductID);
            Assert.Equal(2, p1.ProductID);
            Assert.Equal(3, p1.ProductID);
        }
        [Fact]
        public void Cannot_Edit_NonExistent_Product()
        {
            //Arrange - Create the mock repository
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.products).Returns(new Product[]
            {
                new Product{ProductID = 1, Name = "p1"},
                new Product{ProductID = 2, Name = "p2"},
                new Product{ProductID = 3, Name = "p3"}
            }.AsQueryable<Product>());

            //Arrange - Create the controller
            AdminController target = new AdminController(mock.Object);
            //Act
            Product result = GetViewModel<Product>(target.Edit(4));
            //Assert
            Assert.Null(result);
        }
        [Fact]
        public void Can_Save_Valid_Changes()
        {
            //Arrange - Create mock repository
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            //Arrange - create mock TempData
            Mock<ITempDataDictionary> tempData = new Mock<ITempDataDictionary>();
            //Arrange - Create the controller
            AdminController target = new AdminController(mock.Object)
            {
                TempData = tempData.Object
            };
            //Arrange - Create a product
            Product product = new Product { Name = "Test" };
            //Act - Try to save the product
            IActionResult result = target.Edit(product);

            //Assert - check that the repository was called
            mock.Verify(m => m.SaveProduct(product));
            //Assert - Check the result type is a redirection
            Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", (result as RedirectToActionResult).ActionName);
            
        }
        [Fact]
        public void Cannot_Save_Invalid_Changes()
        {
            //Arrange - Create mock repository
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            //Arrange - create the controller
            AdminController target = new AdminController(mock.Object);
            //Arrange - Create a product
            Product product = new Product { Name = "Test" };
            //Arrange - Add an error to the Model State
            target.ModelState.AddModelError("error", "error");

            //Act - Try to save the product
            IActionResult result = target.Edit(product);
            //Assert - Check that the repository was not called
            mock.Verify(m => m.SaveProduct(It.IsAny<Product>()), Times.Never());
            //Assert - Check the method result type
            Assert.IsType<ViewResult>(result);

        }
        [Fact]
        public void Can_Delete_Valid_Products()
        {
            // Arrange - Create a Product
            Product prod = new Product { ProductID = 2, Name = "Test" };
            // Arrange - Create the mock repository
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.products).Returns(new Product[]
            {
                new Product {ProductID = 1, Name = "p1"},
                prod,
                new Product{ProductID = 3, Name = "p3"}
            }.AsQueryable<Product>());

            //Arrange - Create the controller
            AdminController target = new AdminController(mock.Object);
            //Act - Delete the product
            target.Delete(prod.ProductID);

            //Assert - Ensure that the repository delete Method was called with the correct Product
            mock.Verify(m => m.DeleteProduct(prod.ProductID));
        }
    }
}
