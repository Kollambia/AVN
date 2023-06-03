using AVN.Common.Enums;

namespace AVN.Model.Entities
{
    public class OrderType : BaseEntity<OrderType, int>
    {
        public string Name { get; set; }
        public int? MovementTypeId { get; set; }
        public virtual MovementType? MovementType { get; set; }
        public virtual ICollection<Order>? Orders { get; set; }

        public OrderType()
        {
            Orders = new List<Order>();
        }
    }
}
