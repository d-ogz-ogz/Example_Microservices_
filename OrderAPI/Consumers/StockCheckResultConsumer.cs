using MassTransit;
using SHARED.Contracts;
using SHARED.Enums;
using SHARED.Events;

namespace OrderAPI.Consumers
{
    public class StockCheckResultConsumer : IConsumer<StockCheckResultEvent>
    {
        private readonly IPublishEndpoint _endpoint;
        private readonly IUnitOfWork _uow;

        public StockCheckResultConsumer(IPublishEndpoint endpoint,IUnitOfWork uow)
        {
            _endpoint = endpoint;
            _uow = uow;
        }
        public async Task Consume(ConsumeContext<StockCheckResultEvent> context)
        {
            var order = await _uow.Orders.GetById(Convert.ToInt32(context.Message.OrderId));
            var stockRes = context.Message;
            if (!stockRes.IsSuccess) {
                order.OrderStatus = OrderStatus.Failed;
                await _uow.Orders.SaveChangesAsync();
            }
            await _endpoint.Publish(

                new OrderCreatedEvent
                {
                    OrderId = stockRes.OrderId,
                    Items = stockRes.Items
                });
          
            }
        } 
    }

