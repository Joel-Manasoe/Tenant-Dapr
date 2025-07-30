using Common;
using Common.DTOs;
using Dapr.Actors;
using Dapr.Actors.Client;
using Dapr.Client;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Tenant.Dapr.Actors;

namespace Tenant.Dapr.Controllers
{
    [Route("api/[controller]/[action]")]
    [EnableCors("CorsPolicy")]
    public class TenantController : Controller
    {
        private readonly DaprClient _daprClient;
        private readonly ILogger<TenantController> _logger;
        private readonly IActorProxyFactory _actorProxyFactory;
        public TenantController(DaprClient daprClient, ILogger<TenantController> logger, IActorProxyFactory actorProxyFactory)
        {
            this._daprClient = daprClient;
            _logger = logger;
            _actorProxyFactory = actorProxyFactory;
        }

        /// <summary>
        /// Register Tenant to the system.
        /// </summary>
        /// <param name="tenant">Object that contain Tenant information.</param>
        /// <returns>Tenant state information if successful.</returns>
        [HttpPost]
        public async Task<IActionResult> AddTenant([FromBody] TenantDto tenant)
        {
            var respond = new Respond();
            // create tenant actor proxy
            var tenantActorState= _actorProxyFactory.CreateActorProxy<ITenantActor>(
                    new ActorId(tenant.Id.ToString()), nameof(TenantActor));
            try
            {
                // call method of actor so that they can be registered into the system
                var tenantActor = await tenantActorState.AddTenantAsync(tenant);
                if (tenantActor == null)
                {
                    respond.Message = "Failed to add tenant.";
                    _logger.LogWarning("Failed to add tenant ");
                    return BadRequest(respond);
                }
                respond.Message = "Tenant added successfully.";
                respond.Success = true;
                respond.Results = tenantActor;
                _logger.LogWarning("Tenant was successfully added with ID: {TenantId}", tenant.Id);
                return Ok(respond);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to add tenant with ID: {TenantId}", tenant.Id);
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Get tenant state actor results.
        /// </summary>
        /// <param name="tenantId">Pass the tenant ID.</param>
        /// <returns>Retun Tenant state information if successful.</returns>
        [HttpGet("{tenantId}")]
        public async Task<IActionResult> GetState(string tenantId)
        {
            try
            {
                // create tenant actor proxy
                var tenantActorProxy =  _actorProxyFactory.CreateActorProxy<ITenantActor>(
                new ActorId(tenantId), nameof(TenantActor)
            );
                var TenantResults = await tenantActorProxy.GetTenantStateAsync();
                // check whether tenant exist in the system.
                if (TenantResults == null)
                {
                    return NotFound(new { message = "Tenant '{tenantId}' not found.", tenantId });
                }
                return Ok(TenantResults);
            }
            catch (Exception ex)
            {
                // Optionally log ex.Message
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }
        }
    }
}
