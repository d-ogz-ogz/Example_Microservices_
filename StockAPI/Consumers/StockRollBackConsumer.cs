using MassTransit;
using Microsoft.EntityFrameworkCore;
using SHARED;
using SHARED.Contracts;
using SHARED.Events;

namespace StockAPI.Consumers
{
    public class StockRollBackConsumer : IConsumer<StockRollBackEvent>
    {
        private readonly IUnitOfWork _uow;
        private readonly ISendEndpointProvider _sendEndpointProvider;
        public StockRollBackConsumer(IUnitOfWork uow, ISendEndpointProvider sendEndpointProvider)
        {
            _uow = uow;
            _sendEndpointProvider = sendEndpointProvider;
        }
        public async Task Consume(ConsumeContext<StockRollBackEvent> context)
        {

            foreach (var oi in context.Message.OrderItems)
            {
                var stock = await (await _uow.Stock_Details.GetAllAsync(o => o.ItemId == oi.ItemId)).FirstOrDefaultAsync();
                if (stock != null)
                {
                    stock.Count += oi.Quantity;
                    _uow.Stock_Details.Update(stock);
                    await _uow.Stock_Details.SaveChangesAsync();

                }
              
            }
            var paymentNotCompletedEndPoint = await (_sendEndpointProvider.GetSendEndpoint(new Uri(RabbitMQConstants.QueueNames.PaymentNotCompleted)));
            await paymentNotCompletedEndPoint.Send(new PaymentNotCompletedEvent
            {
                RelatedOrderId=context.Message.OrderId,
                Currency="tl",
                Timestamp= DateTime.Now,
                Amount=context.Message.Amount,
                CustomerEmail=context.Message.CustomerEmail,

            });
        }
    }
}

