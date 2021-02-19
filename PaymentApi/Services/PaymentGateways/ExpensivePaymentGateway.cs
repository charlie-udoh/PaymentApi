using PaymentApi.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace PaymentApi.Services.PaymentGateways
{
    public class ExpensivePaymentGateway: IExpensivePaymentGateway
    {
        public async Task<PaymentGatewayResponse> ProcessPayment(string creditCardNumber, string cardHolder, decimal amount)
        {
            var result = new PaymentGatewayResponse();
            using (var httpClient = new HttpClient())
            {
                httpClient.Timeout = System.Threading.Timeout.InfiniteTimeSpan;
                try
                {
                    var data = new JsonContent(new { creditCardNumber, cardHolder, amount });
                    using (HttpResponseMessage res = await httpClient.PostAsync("https://somedummyurl", data))
                    {
                        using (HttpContent content = res.Content)
                        {
                            var response = await content.ReadAsStringAsync();
                            res.EnsureSuccessStatusCode();
                            //this section depends on the response structure from the service
                            result.StatusCode = 200;
                            result.PaymentStatus = "";
                            result.Message = "";
                        }
                    }
                }
                catch (Exception ex)
                {
                    result.StatusCode = 500;
                    result.PaymentStatus = "Failed";
                    result.Message = ex.ToString();
                }
            }
            return result;
        }
    }
}
