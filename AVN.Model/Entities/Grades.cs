namespace AVN.Model.Entities
{
    public class Grade
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public string? StudentId { get; set; }
        public virtual Student Student { get; set; }
        public string? EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
