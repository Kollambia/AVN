using AVN.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVN.Model.Entities
{
    public class Setting : BaseEntity<Setting, int>
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public string? Description { get; set; }
    }
}
