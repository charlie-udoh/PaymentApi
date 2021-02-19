namespace PaymentApi.Entities
{
    public class PaymentLog : BaseEntity
    {
        public int PaymentId { get; set; }
        public string Status { get; set; }
        public virtual Payment Payment { get; set; }
    }
}
