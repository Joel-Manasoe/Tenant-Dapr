using Common;
using Common.DTOs;
using Dapr.Client;
using Microsoft.AspNetCore.Mvc;

namespace Payment.Controllers
{
    [Route("api/[controller]/[action]")]
    public class PaymentController : Controller
    {
        private readonly DaprClient _daprClient;
        private readonly ILogger<PaymentController> _logger;

        public PaymentController(DaprClient daprClient, ILogger<PaymentController> logger)
        {
            _daprClient = daprClient;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> MakePayment([FromBody] PayBill pay)
        {
            var respond = new Respond();
            _logger.LogInformation("Payment from Id {TenantId}", pay.TenantId);
            if (pay is not null)
            {
                await _daprClient.PublishEventAsync("pubsub", "paytopic", pay);
                _logger.LogInformation("Payment went successfully from tenant Id {TenantId}", pay.TenantId);
                respond.Message = "Payment published successfully.";
                respond.Success = true;
                respond.Results = pay;
                return Ok(respond);
            }
            return BadRequest();
        }
    }
}
