using Orders.API.Services;
using Xunit;

namespace Tests.Services
{
    public class OrderServiceTests
    {

        [Trait("Order", "Service")]
        [Theory(DisplayName = " OrderService")]
        [InlineData("morning, 1, 2, 3", "eggs, toast, coffee")]
        [InlineData("morning, 2, 1, 3", "eggs, toast, coffee")]
        [InlineData("morning, 1, 2, 3, 4", "eggs, toast, coffee, error")]
        [InlineData("morning, 1, 2, 3, 3, 3", "eggs, toast, coffee(x3)")]

        [InlineData("night, 1, 2, 3, 4", "steak, potato, wine, cake")]
        [InlineData("night, 1, 2, 2, 4", "steak, potato(x2), cake")]
        [InlineData("night, 1, 2, 3, 5", "steak, potato, wine, error")]
        [InlineData("night, 1, 1, 2, 3, 5", "steak, error")]

        [InlineData("", "Invalid Order, Blank Order")]
        [InlineData("!#,1,1 ", "Invalid Order, special characters are not allowed")]
        [InlineData("night, 1, ", "Invalid Order, Expected Values After a Comma")]
        [InlineData("1, 1, 2", "Invalid Order, First Expected Value:, Time of Day")]
        [InlineData("asdfs, 1, 2", "Invalid Order, Available Service Period: Morning and Night")]        
        [InlineData("night", "Invalid Order, No Dishes")]

        public void GetOrder_Validate_Response(string _order, string _resultExpected)
        {
           // Arrange
             OrderService service = new OrderService();

            //Act
            var result = service.ValidateOrder(_order);

            if (string.IsNullOrEmpty(result))
                result = service.GetOrder(_order);

            //Assert
             Assert.Equal(_resultExpected, result);

        }
    }
}
