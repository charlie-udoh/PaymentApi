namespace PaymentApi.Models
{
    public class PaymentGatewayResponse
    {
        public int StatusCode { get; set; }
        public string PaymentStatus { get; set; }
        public string Message { get; set; }
    }
}
