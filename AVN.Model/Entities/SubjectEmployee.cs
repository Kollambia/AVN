namespace AVN.Model.Entities
{
    public class SubjectEmployee : BaseEntity
    {
        public int EmployeeCreditHours { get; set; }
        public int? SubjectId { get; set; }
        public int? EmployeeId { get; set; }
        public virtual Subject Subject { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
