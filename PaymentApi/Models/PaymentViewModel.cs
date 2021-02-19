using PaymentApi.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace PaymentApi.Models
{
    public class PaymentViewModel
    {
        [Required, CreditCard]
        public string CreditCardNumber { get; set; }

        [Required]
        public string CardHolder { get; set; }

        [Required, CurrentDate(ErrorMessage = "Date must be after or equal to current date")]
        public DateTime ExpirationDate { get; set; }

        [StringLength(3, ErrorMessage = "The {0} value cannot exceed {1} characters. ")]
        public string SecurityCode { get; set; }

        [Required, Range(1, double.MaxValue, ErrorMessage = "Value cannot be a negative number")]
        public decimal Amount { get; set; }
    }
}
