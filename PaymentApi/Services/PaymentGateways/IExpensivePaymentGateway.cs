using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentApi.Services.PaymentGateways
{
    public interface IExpensivePaymentGateway
    {
        public bool ProcessPayment(string creditCardNumber, string cardHolder, decimal amount);
    }
}
