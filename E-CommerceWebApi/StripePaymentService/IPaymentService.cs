using Stripe;

namespace E_CommerceWebApi.StripePaymentService
{
    public interface IPaymentService
    {
        public Task<PaymentIntent> CreatePaymentIntent(decimal amount, string currency);
        public Task<PaymentIntent> ConfirmPaymentIntent(string paymentIntentId);    
    }
}
