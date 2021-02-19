using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentApi.Services.PaymentGateways
{
    public class ExpensivePaymentGateway: IExpensivePaymentGateway
    {
        public bool ProcessPayment(string creditCardNumber, string cardHolder, decimal amount)
        {
            return true;
        }
    }
}
