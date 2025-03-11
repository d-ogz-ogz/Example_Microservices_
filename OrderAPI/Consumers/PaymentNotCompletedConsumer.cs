using MassTransit;
using Microsoft.AspNetCore.Http.HttpResults;
using SHARED.Contracts;
using SHARED.Events;
using SHARED.Services;

namespace OrderAPI.Consumers
{
    public class PaymentNotCompletedConsumer : IConsumer<PaymentNotCompletedEvent>
    {
        private readonly IUnitOfWork _uow;
        private readonly EmailService _emailSender;

        public PaymentNotCompletedConsumer(IUnitOfWork uow,EmailService emailSender)
        {
            _uow = uow;
            _emailSender = emailSender;
        }
        public async Task Consume(ConsumeContext<PaymentNotCompletedEvent> context)
        {
            var failuredOrder = context.Message;
            var customerEmail= context.Message.CustomerEmail;
            var mailSubject = "Order Failure";
            var mailBody = $" your order payment failed : {failuredOrder.Amount}{failuredOrder.Currency } /n {failuredOrder.Timestamp}";
            var order =await _uow.Orders.GetById(Convert.ToInt32(context.Message.RelatedOrderId));
            if (order==null)
            {
               throw new ArgumentNullException(nameof(context));
            }
            await _emailSender.SendEmailAsync(customerEmail, mailBody, mailSubject);

           
        }
    }
}
