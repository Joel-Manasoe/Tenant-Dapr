namespace Common
{
    public class PayBill
    {
        public PayBill()
        {
            PaymentMethod = string.Empty;
            TenantId = string.Empty;
        }

        public string TenantId { get; set; }
        public int Amount { get; set; }
        public string PaymentMethod { get; set; }
    }
}
