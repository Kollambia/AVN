namespace AVN.Model.Entities
{
    public class SubjectEmployee : BaseEntity
    {
        public int SubjectId { get; set; }
        public int EmployeeId { get; set; }
        public int EmployeeCreditHours { get; set; }
        public Subject Subject { get; set; }
        public Employee Employee { get; set; }
    }
}
