using Domain.Common;

namespace Domain.Entities
{
    public class PurchaseOrder : EntityBase<int>
    {
        public decimal Amount { get; set; }
        public string Number { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
