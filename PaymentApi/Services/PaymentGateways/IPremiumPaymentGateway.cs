﻿using PaymentApi.Models;
using System.Threading.Tasks;

namespace PaymentApi.Services.PaymentGateways
{
    public interface IPremiumPaymentGateway
    {
        public Task<PaymentGatewayResponse> ProcessPayment(string creditCardNumber, string cardHolder, decimal amount);
    }
}
