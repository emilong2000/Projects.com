using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Moq;
using SportStore.Infrastructure;
using SportStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SportStore.Tests
{
    public class PageLinkTagHelperTests
    {
        [Fact]
        public void Can_generate_Page_Links()
        {
            //Arrage
            var urlHelper = new Mock<IUrlHelper>();
            urlHelper.SetupSequence(m => m.Action(It.IsAny<UrlActionContext>()))
            .Returns("Test/Page1")
            .Returns("Test/page2")
            .Returns("Test/Page3");

            var urlFactory = new Mock<IUrlHelperFactory>();
            urlFactory.Setup(m => m.GetUrlHelper(It.IsAny<ActionContext>()))
                .Returns(urlHelper.Object);

            PageLinkTagHelper helper = new PageLinkTagHelper(urlFactory.Object)
            {
                PageModel = new PagingInfo { CurrentPage = 2, ItemsPerPage = 28, TotalItems = 10 },
                PageAction = "Test"
            };

            TagHelperContext ctx = new TagHelperContext(
                new TagHelperAttributeList(),
                new Dictionary<object, Object>(), "");

            var content = new Mock<TagHelperContent>();
            TagHelperOutput output = new TagHelperOutput("div",
                new TagHelperAttributeList(),
                (cache, encoder) => Task.FromResult(content.Object));

            //Act
            helper.Process(ctx, output);

            //Assert
            Assert.Equal
            (@"<a href=""Test/Page1"">1</a>"
            + @"<a href=""Test/Page2"">2</a>"
            + @"<a href=""Test/Page2"" > 3 </a>",
            output.Content.GetContent());


        }
    }
}
