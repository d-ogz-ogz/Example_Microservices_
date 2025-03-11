using MassTransit;
using SHARED;
using SHARED.Contracts;
using SHARED.Events;

namespace StockAPI.Consumers
{
    public class StockCheckConsumer : IConsumer<StockCheckEvent>
    {
        private readonly IUnitOfWork _uow;
        private readonly ISendEndpointProvider _endpointProvider;
        public StockCheckConsumer(ISendEndpointProvider endpointProvider, IUnitOfWork uow)
        {
            _endpointProvider = endpointProvider;
            _uow = uow;
        }
        public async Task Consume(ConsumeContext<StockCheckEvent> context)
        {
            var errorMessage = "";
            var stockItems = context.Message.Items;
            foreach (var item in stockItems)
            {
                var stockData = await _uow.Stock_Details.GetById(Convert.ToInt32(item.ItemId));
                if (item != null && stockData.Count > 0)
                {
                    stockData.Count -= item.Quantity;
                    _uow.Stock_Details.Update(stockData);
                    await _uow.Stock_Details.SaveChangesAsync();

                }
                else
                {
                     errorMessage = " Stock is insufficient";
                }

                var sendPoint = await _endpointProvider.GetSendEndpoint(new Uri(RabbitMQConstants.QueueNames.StockCheck));
                await sendPoint.Send(new StockCheckResultEvent
                {
                    OrderId = context.Message.OrderId,
                    IsSuccess = true,
                    Items = context.Message.Items,
                    ErroMessage = errorMessage



                });
            }

        }


    }
}

