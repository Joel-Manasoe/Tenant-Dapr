
using Common;
using Dapr;
using Dapr.Client;
using Microsoft.AspNetCore.Mvc;

namespace PaymentReceived.Controllers
{
    [ApiController]
    [Route("receive")]
    public class ReceiveController : ControllerBase
    {
        private readonly ILogger<ReceiveController> _logger;
        private readonly DaprClient _daprClient;

        public ReceiveController(ILogger<ReceiveController> logger, DaprClient daprClient)
        {
            _logger = logger;
            _daprClient = daprClient;
        }

        [Topic("pubsub", "paytopic")]
        [HttpPost]
        public async Task<IActionResult> ReceivePayment([FromBody] PayBill payment)
        {
            _logger.LogWarning("Received payment from tenant: {TenantId}", payment.TenantId);

            var tenantState = await _daprClient.InvokeMethodAsync<RentState>(
                HttpMethod.Get, "tenant", $"api/Tenant/GetState/{payment.TenantId}");

            if (tenantState == null)
            {
                _logger.LogWarning("No tenant found with ID {TenantId}", payment.TenantId);
                return NotFound();
            }

            _logger.LogInformation("Tenant: {Name}, Amount Paid: {Amount}, Method: {Method}",
                tenantState.Name, payment.Amount, payment.PaymentMethod);

            return Ok(tenantState);
        }
    }
}
