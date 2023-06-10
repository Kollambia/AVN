namespace AVN.Model.Entities
{
    public class StudentMovement: BaseEntity<StudentMovement, int>
    {
        public int OldGroupId { get; set; }
        public int NewGroupId { get; set; }
        public DateTime MovementDate { get; set; }
        public string OrderNumber { get; set; }
        public int? MovementTypeId { get; set; }
        public virtual MovementType? MovementType { get; set; }
        public int? AcademicYearId { get; set; }
        public virtual AcademicYear? AcademicYear { get; set; }
        public int? StudentId { get; set; }
        public virtual Student? Student { get; set; }

    }
}
