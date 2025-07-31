using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class PaymentEvent
    {
        public PaymentEvent()
        {
            TenantId = string.Empty;
            Name = string.Empty;
            PaymentMethod = string.Empty;
            DueDate = string.Empty;
        }
        public Guid Id { get; set; } = Guid.NewGuid();
        public string TenantId { get; set; }
        public string Name { get; set; }
        public int RentalAmount { get; set; }
        public int Amount { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public string DueDate { get; set; }
        public string PaymentMethod { get; set; }
        public int Outstanding { get; set; }
    }
}
