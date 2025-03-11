using MassTransit;
using Microsoft.AspNetCore.Mvc;
using SHARED;
using SHARED.Enums;
using SHARED.Events;

namespace OrderAPI.Controllers
{
    public class OrderController : Controller
    {
        private readonly ISendEndpointProvider _sendEndpointProvider;
        public OrderController(ISendEndpointProvider sendEndpointProvider)
        {
            _sendEndpointProvider = sendEndpointProvider;
        }
        [HttpPost("CreateOrder")]
        public async Task<IActionResult> CreateOrder(OrderCreatedEvent order)
        {
            var endpoint =await _sendEndpointProvider.GetSendEndpoint(new Uri(RabbitMQConstants.QueueNames.StockCheck));
            var stockCheck = new StockCheckEvent
            {
                OrderId = Guid.NewGuid(),
                Items = order.Items,
               
             

            };
            await endpoint.Send(stockCheck);    
          
            return Ok(new { Message = "OK!" });
        }
    }
}
 