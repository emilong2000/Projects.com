using Microsoft.AspNetCore.Mvc;
using Moq;
using SportStore.Controllers;
using SportStore.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SportStore.Tests
{
    public class OrderControllerTest
    {
        [Fact]
        public void Cannot_Checkout_Empty_Cart()
        {
            //Arrange - Create a mock repository
            Mock<IOrderRepository> mock = new Mock<IOrderRepository>();
            // Create an empty cart
            Cart cart = new Cart();
            // Arrange - Create Order
            Order order = new Order();
            // Arrange - Create the instance of the controller
            OrderController target = new OrderController(mock.Object, cart);
            //Act
            ViewResult result = target.Checkout(order) as ViewResult;

            //Assert - Check that the order hasn't been stored
            mock.Verify(m => m.SaveOrder(It.IsAny<Order>()), Times.Never);
            //Assert - Check that the method is returning to the default view
            Assert.True(string.IsNullOrEmpty(result.ViewName));
            //Assert - Check that i am passing an invalid model to the view
            Assert.False(result.ViewData.ModelState.IsValid);
             
        }
        [Fact]
        public void Cannot_Checkout_Valid_ShippingDetails()
        {
            //Arrange - Create a mock order repository
            Mock<IOrderRepository> mock = new Mock<IOrderRepository>();
            //Arrange - Create a cart with one item
            Cart cart = new Cart();
            cart.AddItem(new Product(), 1);
            //Arrange - create an instance of the controller
            OrderController target = new OrderController(mock.Object, cart);
            //Arrange - Add an error to the model
            target.ModelState.AddModelError("Error", "error");

            //Try to checkout
            ViewResult result = target.Checkout(new Order()) as ViewResult;

            //Assert - Check that the order hasn't been passed stored
            mock.Verify(m => m.SaveOrder(It.IsAny<Order>()), Times.Never);

            //Assert - check that the method is returning the default view
            Assert.True(string.IsNullOrEmpty(result.ViewName));
            //Assert - check that i am passing an invalid mod3el to the view
            Assert.False(result.ViewData.ModelState.IsValid);
        }
        [Fact]
        public void Can_Checkout_And_Submit_Order()
        {
            //Arrange - Create a mock Order repository
            Mock<IOrderRepository> mock = new Mock<IOrderRepository>();
            //Arrange - Create a cart with one Item
            Cart cart = new Cart();
            cart.AddItem(new Product(), 1);
            //Arrange - Create instance of the controller
            OrderController target = new OrderController(mock.Object, cart);
            //Act - try to checkout
            RedirectToActionResult result = target.Checkout(new Order()) as RedirectToActionResult;
            //Assert - check that the order has been stored
            mock.Verify(m => m.SaveOrder(It.IsAny<Order>()), Times.Once);
            //Assert - check that the method is redirecting to complected action
            Assert.Equal("Completed", result.ActionName);

        }
    }
}
