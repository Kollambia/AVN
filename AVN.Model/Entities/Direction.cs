using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVN.Model.Entities
{
    public class Direction : BaseEntity
    {
        public string DirectionName { get; set; }
        public string? DirectionShortName { get; set; }
        public string? Description { get; set; }
        public int DirectionNumber { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        public ICollection<Group> Groups { get; set; }
    }
}
