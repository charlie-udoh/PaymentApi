using Microsoft.AspNetCore.Mvc;
using PaymentApi.Models;
using PaymentApi.Services;
using System.Threading.Tasks;

namespace PaymentApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Hi, welcome to the payment API");
        }

        [HttpPost("process")]
        public async Task<IActionResult> ProcessPayment([FromBody] PaymentViewModel payment)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode(400, ModelState);
                }
                var response = await _paymentService.MakePayment(payment);
                return StatusCode(response.StatusCode, response.Message);
            }
            catch
            {
                return StatusCode(500, "An error has occurred while processing payment");
            }

        }
    }
}
