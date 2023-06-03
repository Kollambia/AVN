using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVN.Model.Entities
{
    public class MovementType : BaseEntity<MovementType, int>
    {
        public string Name { get; set; }

        public virtual ICollection<OrderType>? OrderTypes { get; set; }
        public virtual ICollection<StudentMovement>? StudentMovements { get; set; }
        public virtual ICollection<Order>? Orders { get; set; }

        public MovementType()
        {
            OrderTypes = new List<OrderType>();
            StudentMovements = new List<StudentMovement>();
            Orders = new List<Order>();
        }
    }
}
