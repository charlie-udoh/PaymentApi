using PaymentApi.Models;
using System.Threading.Tasks;

namespace PaymentApi.Services
{
    public interface IPaymentService
    {
        public Task<PaymentServiceResponse> MakePayment(PaymentViewModel payment);
    }
}
