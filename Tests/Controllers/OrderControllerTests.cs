using Microsoft.AspNetCore.Mvc.Infrastructure;
using Orders.API.Controllers;
using System.Net;
using Xunit;

namespace Tests.Controllers
{
    public class OrderControllerTests
    {   
        [Trait("Order", "Controller")]
        [Theory(DisplayName = "Order Controller")]
        [InlineData("morning, 1, 2, 3", HttpStatusCode.OK)]
        [InlineData("night, 1, 2, 3, 4", HttpStatusCode.OK)]
        [InlineData("dawn, 1, 2, 3 ", HttpStatusCode.BadRequest)]
        [InlineData("1, 2, 3", HttpStatusCode.BadRequest)]
        public void GetOrder_Validate_HttpStatusCode(string _order,
                                                    HttpStatusCode statusCode)
        {
            // Arrange
            var _controller = new OrderController();

            // Act   
            var okResult = _controller.GetOrder(_order).Result as IStatusCodeActionResult;

            // Assert            
            Assert.Equal((int)statusCode, okResult.StatusCode);         

        }
    }
}
