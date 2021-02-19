using PaymentApi.Data;
using PaymentApi.Entities;
using PaymentApi.Models;
using PaymentApi.Services.PaymentGateways;

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

        public bool MakePayment(PaymentViewModel payment)
        {
            var result = false;
            if (payment.Amount <= 20)
            {
                result = _cheapPaymentGateway.ProcessPayment(payment.CreditCardNumber, payment.CardHolder, payment.Amount);
            }
            else if (payment.Amount <= 500)
            {
                result = _expensivePaymentGateway.ProcessPayment(payment.CreditCardNumber, payment.CardHolder, payment.Amount);
                if (!result)
                {
                    result = _cheapPaymentGateway.ProcessPayment(payment.CreditCardNumber, payment.CardHolder, payment.Amount);
                }
            }
            else if (payment.Amount > 500)
            {
                int numberOfRetries = 3;
                int i = 0;
                while (i < numberOfRetries)
                {
                    result = _premiumPaymentGateway.ProcessPayment(payment.CreditCardNumber, payment.CardHolder, payment.Amount);
                    if (result)
                    {
                        break;
                    }
                }
            }
            SavePayment(payment, result);
            return false;
        }

        public void SavePayment(PaymentViewModel payment, bool processingResult)
        {
            _unitOfWork.PaymentRepository.Insert(new Payment
            {
                CreditCardNumber = payment.CreditCardNumber,
                CardHolder = payment.CardHolder,
                ExpirationDate = payment.ExpirationDate,
                SecurityCode = payment.SecurityCode,
                Amount = payment.Amount,
                PaymentLog = new PaymentLog { Status = processingResult ? "Processed" : "Pending" }
            });
            _unitOfWork.Commit();
        }
    }
}
