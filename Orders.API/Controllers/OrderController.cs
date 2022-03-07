using Microsoft.AspNetCore.Mvc;
using Orders.API.Services;
using Swashbuckle.AspNetCore.Annotations;
using System;

namespace Orders.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {

        [HttpGet("/Order")]       
        [SwaggerResponse(200,"Success", typeof(String))]
        [SwaggerResponse(400,"Bad Request", typeof(String))]
        public ActionResult<String> GetOrder(string _order)
        {
            var service = new OrderService();

            var Error = service.ValidateOrder(_order);

            if (!string.IsNullOrEmpty(Error))
                return BadRequest(Error);

            var result = service.GetOrder(_order);           

            return Ok(result);           

        }
      
    }
}
