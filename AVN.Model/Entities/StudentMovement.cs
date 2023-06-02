using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVN.Model.Entities
{
    public class StudentMovement: BaseEntity<StudentMovement, int>
    {
        public int StudentId { get; set; }
        public int OldGroupId { get; set; }
        public int NewGroupId { get; set; }
        public DateTime MovementDate { get; set; }

    }
}
