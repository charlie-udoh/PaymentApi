using PaymentApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentApi.Services
{
    public interface IPaymentService
    {
        public bool MakePayment(PaymentViewModel payment);
    }
}
