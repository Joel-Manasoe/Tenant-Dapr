using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTOs
{
    public class TenantDto
    {
        public TenantDto() {
            Name = string.Empty;
        }
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public int RentalAmount { get; set; }
    }
}
