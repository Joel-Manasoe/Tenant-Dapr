using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class RentState
    {
        public RentState()
        {
            TenantId = string.Empty;
            Name = string.Empty;
        }
        public string TenantId { get; set; }
        public string Name { get; set; }
        public int RentalAmount { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
