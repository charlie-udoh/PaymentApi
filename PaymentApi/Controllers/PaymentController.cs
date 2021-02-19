using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PaymentApi.Models;
using PaymentApi.Services;

namespace PaymentApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly ILogger<PaymentController> _logger;
        private readonly IPaymentService _paymentService;

        public PaymentController(ILogger<PaymentController> logger, IPaymentService paymentService)
        {
            _logger = logger;
            _paymentService = paymentService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Hi, welcome to the payment API");
        }

        [HttpPost("process")]
        public IActionResult ProcessPayment([FromBody] PaymentViewModel payment)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                var response = _paymentService.MakePayment(payment);
                if (!response)
                    return StatusCode(500, "Sorry, we are unable to process your payment at this time");
                return Ok();
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode(500, "An error has occurred while processing payment");
            }
            
        }
    }
}
