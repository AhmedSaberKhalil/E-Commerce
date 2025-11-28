namespace E_CommerceWebApi.Models.Payments
{
    public class CreatePaymentIntentRequest
    {
        public decimal Amount { get; set; }
        public string Currency { get; set; }
    }
}
