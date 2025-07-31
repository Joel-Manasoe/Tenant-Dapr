using Common;
using Common.DTOs;
using Dapr.Actors;

namespace Tenant.Dapr.Actors
{
    public interface ITenantActor : IActor
    {
        Task<RentState> AddTenantAsync(TenantDto tenant);
        Task<RentState> GetTenantStateAsync();
        Task ProcessPaymentAsync(PayBill payBill);
    }
}
