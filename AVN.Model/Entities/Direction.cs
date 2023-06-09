﻿using AVN.Common.Enums;

namespace AVN.Model.Entities
{
    public class Direction : BaseEntity<Direction, int>
    {
        public string DirectionName { get; set; }
        public string? DirectionShortName { get; set; }
        public string? Description { get; set; }
        public int DirectionNumber { get; set; }
        public decimal CreditCost { get; set; }
        public AcademicDegree AcademicDegree { get; set; }
        public TrainingPeriod TrainingPeriod { get; set; } 

        public int? DepartmentId { get; set; }
        public virtual Department Department { get; set; }

        public virtual ICollection<Group> Groups { get; set; }

        public Direction()
        {
            Groups = new List<Group>();
        }
    }
}
