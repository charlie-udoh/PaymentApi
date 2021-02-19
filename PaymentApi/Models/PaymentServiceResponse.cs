using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentApi.Models
{
    public class PaymentServiceResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
    }
}
