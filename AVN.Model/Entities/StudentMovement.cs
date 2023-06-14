namespace AVN.Model.Entities
{
    public class StudentMovement: BaseEntity<StudentMovement, int>
    {
        public string OldGroupId { get; set; }
        public virtual Group? OldGroup { get; set; }
        public string NewGroupId { get; set; }
        public virtual Group? NewGroup { get; set; }
        public DateTime MovementDate { get; set; }
        public string OrderNumber { get; set; }
        public int? MovementTypeId { get; set; }
        public virtual MovementType? MovementType { get; set; }
        public int? AcademicYearId { get; set; }
        public virtual AcademicYear? AcademicYear { get; set; }
        public string? StudentId { get; set; }
        public virtual Student? Student { get; set; }

    }
}
