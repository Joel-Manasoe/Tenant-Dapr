using Common;
using Common.DTOs;
using Dapr.Actors.Runtime;

namespace Tenant.Dapr.Actors
{
    public class TenantActor : Actor, ITenantActor, IRemindable
    {
        public TenantActor(ActorHost host) : base(host) { Logger.LogInformation("🚀 TenantActor constructed"); }

        public async Task<RentState> AddTenantAsync(TenantDto tenant)
        {
            var tenantState = new RentState
            {
                Name = tenant.Name,
                RentalAmount = tenant.RentalAmount,
                TenantId = tenant.Id.ToString(),
                DateCreated = DateTime.UtcNow,
            };
            await StateManager.SetStateAsync("rentstate", tenantState);
            Logger.LogInformation("Added state wiating to registered reminder");

            var reminderName = "MonthlyReminder";
            // Set an initial delay and recurrence
            var dueTime = TimeSpan.FromSeconds(15);
            var period = TimeSpan.FromDays(1);

            // registering reminder
            await RegisterReminderAsync(reminderName, null, dueTime, period);
            Logger.LogInformation("Reminder Registered");
            return tenantState;
        }

        public async Task<RentState> GetTenantStateAsync()
        {
            var state = await StateManager.TryGetStateAsync<RentState>("rentstate");
            return state.Value;
        }

        public Task ProcessPaymentAsync(PayBill payBill)
        {
            throw new NotImplementedException();
        }


        public async Task ReceiveReminderAsync(string reminderName, byte[] state, TimeSpan dueTime, TimeSpan period)
        {
            Logger.LogInformation("Sending monthly rent notification to tenant");
            await Task.CompletedTask;
        }
    }
}
