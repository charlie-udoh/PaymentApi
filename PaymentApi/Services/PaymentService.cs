using PaymentApi.Data;
using PaymentApi.Entities;
using PaymentApi.Models;
using PaymentApi.Services.PaymentGateways;
using System.Threading.Tasks;

namespace PaymentApi.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly ICheapPaymentGateway _cheapPaymentGateway;
        private readonly IExpensivePaymentGateway _expensivePaymentGateway;
        private readonly IPremiumPaymentGateway _premiumPaymentGateway;
        private readonly IUnitOfWork _unitOfWork;

        public PaymentService(ICheapPaymentGateway cheapPaymentGateway, IExpensivePaymentGateway expensivePaymentGateway, IPremiumPaymentGateway premiumPaymentGateway, IUnitOfWork unitOfWork)
        {
            _cheapPaymentGateway = cheapPaymentGateway;
            _expensivePaymentGateway = expensivePaymentGateway;
            _premiumPaymentGateway = premiumPaymentGateway;
            _unitOfWork = unitOfWork;
        }

        public async Task<PaymentServiceResponse> MakePayment(PaymentViewModel payment)
        {
            var gatewayResult = new PaymentGatewayResponse();
            if (payment.Amount <= 20)
            {
                gatewayResult = await _cheapPaymentGateway.ProcessPayment(payment.CreditCardNumber, payment.CardHolder, payment.Amount);
            }
            else if (payment.Amount <= 500)
            {
                gatewayResult = await _expensivePaymentGateway.ProcessPayment(payment.CreditCardNumber, payment.CardHolder, payment.Amount);
                if (gatewayResult.StatusCode != 200)
                {
                    gatewayResult = await _cheapPaymentGateway.ProcessPayment(payment.CreditCardNumber, payment.CardHolder, payment.Amount);
                }
            }
            else if (payment.Amount > 500)
            {
                int numberOfRetries = 3;
                int i = 0;
                while (i < numberOfRetries)
                {
                    gatewayResult = await _premiumPaymentGateway.ProcessPayment(payment.CreditCardNumber, payment.CardHolder, payment.Amount);
                    if (gatewayResult.StatusCode == 200)
                    {
                        break;
                    }
                    i++;
                }
            }
            if(gatewayResult.StatusCode == 200)
                SavePayment(payment, gatewayResult.PaymentStatus);
            var result = new PaymentServiceResponse { StatusCode = gatewayResult.StatusCode, Message = gatewayResult.Message };
            
            return result;
        }

        public void SavePayment(PaymentViewModel payment, string paymentStatus)
        {
            _unitOfWork.PaymentRepository.Insert(new Payment
            {
                CreditCardNumber = payment.CreditCardNumber,
                CardHolder = payment.CardHolder,
                ExpirationDate = payment.ExpirationDate,
                SecurityCode = payment.SecurityCode,
                Amount = payment.Amount,
                PaymentLog = new PaymentLog { Status = paymentStatus }
            });
            _unitOfWork.Commit();
        }
    }
}
