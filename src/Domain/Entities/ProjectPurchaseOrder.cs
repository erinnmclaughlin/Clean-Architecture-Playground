using Domain.Common;

namespace Domain.Entities
{
    public class ProjectPurchaseOrder : EntityBase
    {
        public int ProjectId { get; set; }
        public Project Project { get; set; }

        public int PurchaseOrderId { get; set; }
        public PurchaseOrder PurchaseOrder { get; set; }

        /// <summary>
        /// The amount of money from the purchase order that should be applied to the project.
        /// </summary>
        public decimal Amount { get; set; }
    }
}