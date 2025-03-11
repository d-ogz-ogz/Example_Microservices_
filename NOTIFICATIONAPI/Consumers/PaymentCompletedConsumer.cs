using MassTransit;
using SHARED;
using SHARED.Events;
using SHARED.Services;

namespace NotificationAPI.Consumers
{
    

    public class PaymentCompletedConsumer : IConsumer<PaymentCompletedEvent>
    {
        private readonly ISendEndpointProvider _sendEndpointProvider;
        private readonly EmailService _emailSender;

        public PaymentCompletedConsumer(ISendEndpointProvider sendEndpointProvider, EmailService emailSender)
        {
            _sendEndpointProvider = sendEndpointProvider;
            _emailSender = emailSender;
        }


        public async Task Consume(ConsumeContext<PaymentCompletedEvent> context)
        {

            var Notification = context.Message;
            var successMessage = "YOUR ORDER HAS BEEN RECEIVED";
            var subject = "ABOUT YOUR ORDER";
            await _emailSender.SendEmailAsync(Notification.CustomerEmail, successMessage, subject);
        }
        
    }
}
