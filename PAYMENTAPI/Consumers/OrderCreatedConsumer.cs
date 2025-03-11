using MassTransit;
using MassTransit.Transports;
using SHARED;
using SHARED.Events;
using SHARED.Services;

namespace PaymentAPI.Consumers
{
   
    public class OrderCreatedConsumer : IConsumer<OrderCreatedEvent>
    {
        private readonly ISendEndpointProvider _sendEndPointProvider;
        private readonly PaymentService _paymentService;
        public OrderCreatedConsumer(ISendEndpointProvider sendEndPointProvider, PaymentService paymentService)
        {
            _sendEndPointProvider = sendEndPointProvider;
            _paymentService = paymentService;
        }

        public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
        {
            var order = context.Message;
            var paymentSuccess= await _paymentService.ChargeAsync(order.OrderId,order.CustomerEmail,order.TotalPrice,"tl");
           
            if (paymentSuccess)
            {
               
               var sendEndpoint2 = await _sendEndPointProvider.GetSendEndpoint(new Uri(RabbitMQConstants.QueueNames.StockRollback));
                await sendEndpoint2.Send(new StockRollBackEvent
                { 
                    OrderId=context.Message.OrderId,
                    OrderItems=context.Message.Items,
                    Amount =context.Message.TotalPrice,
                    CustomerEmail= context.Message.CustomerEmail
                });
         

            }

            var paymentEvent = new PaymentCompletedEvent {
                OrderId = context.Message.OrderId,
                IsSuccessful = paymentSuccess,
                CustomerEmail = context.Message.CustomerEmail
            };
            var sendEndpoint = await _sendEndPointProvider.GetSendEndpoint(new Uri(RabbitMQConstants.QueueNames.PaymentCompleted));
            await sendEndpoint.Send(paymentEvent);
        }
           
    }
}
