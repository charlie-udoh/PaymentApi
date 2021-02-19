using System;

namespace PaymentApi.Entities
{
    public class Payment : BaseEntity
    {
        public string CreditCardNumber { get; set; }
        public string CardHolder { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string SecurityCode { get; set; }
        public decimal Amount { get; set; }
        public virtual PaymentLog PaymentLog { get; set; }
    }
}
