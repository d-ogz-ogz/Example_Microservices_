using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHARED.Services
{
    public class PaymentService
    {
        private readonly Stripe.StripeClient _stripeClient;
        public PaymentService()
        {
            _stripeClient = new Stripe.StripeClient("api key");
        }

        public async Task<bool> ChargeAsync(Guid orderId, string customerEmail, decimal totalAmount, string Currency)
        {
            var options = new Stripe.PaymentIntentCreateOptions
            {
                Amount = (long)totalAmount,
                Currency = Currency,
                ReceiptEmail = customerEmail,
                Description = $"Payment {orderId}"

            };
            var service = new Stripe.PaymentIntentService(_stripeClient);
            Stripe.PaymentIntent paymentIntent = await service.CreateAsync(options);

            if (paymentIntent.Status=="succeded")
            {
                return true;
            }
            else
            {
                return false;
            }

        }

    }
}
