namespace PaymentApi.Services.PaymentGateways
{
    public interface ICheapPaymentGateway
    {
        public bool ProcessPayment(string creditCardNumber, string cardHolder, decimal amount);
    }
}
