using Stripe;
using Stripe.FinancialConnections;

namespace E_CommerceWebApi.StripePaymentService
{
    public class PaymentService : IPaymentService
    {
        public async Task<PaymentIntent> CreatePaymentIntent(decimal amount, string currency)
        {
            var options = new PaymentIntentCreateOptions
            {
                Amount = (long)(amount * 100), // Amount in cents
                Currency = currency,
                PaymentMethodTypes = new List<string> { "card" }
            };

            var service = new PaymentIntentService();
            return await service.CreateAsync(options);
        }
        public async Task<PaymentIntent> ConfirmPaymentIntent(string paymentIntentId)
        {
            var service = new PaymentIntentService();
            return await service.ConfirmAsync(paymentIntentId);
        }
    }
}
