using AVN.Common.Enums;

namespace AVN.Model.Entities
{
    public class Order : BaseEntity<Order, int>
    {
        public DateTime Date { get; set; }
        public string Number { get; set; }
        public string? Note { get; set; }
        public Course Course { get; set; }

        public int MovementTypeId { get; set; }
        public virtual MovementType? MovementType { get; set; }

        public int OrderTypeId { get; set; }
        public virtual OrderType? OrderType { get; set; }

        public int? AcademicYearId { get; set; }
        public virtual AcademicYear? AcademicYear { get; set; }

        public string? GroupId { get; set; }
        public virtual Group? Group { get; set; }

        public string? StudentId { get; set; }
        public virtual Student? Student { get; set; }

    }
}
