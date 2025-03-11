using MassTransit;
using SHARED.Contracts;

using SHARED.Events;

namespace OrderAPI.Consumers
{
    public class NotificationCompletedConsumer : IConsumer<NotificationCompletedEvent>
    {
        private readonly IUnitOfWork _uow;

        public NotificationCompletedConsumer(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task Consume(ConsumeContext<NotificationCompletedEvent> context)
        {
            var order = await _uow.Orders.GetById(Convert.ToInt32(context.Message.OrderId));
            if (context.Message.IsSuccessful)
            {
                order.OrderStatus = SHARED.Enums.OrderStatus.Completed;

            }
            else
            {
                order.OrderStatus = SHARED.Enums.OrderStatus.Failed;
            }
            _uow.Orders.Update(order);
            await _uow.Orders.SaveChangesAsync();
            await Task.FromResult(context.Message.Message);
        }
    }
}
