namespace AVN.Model.Entities
{
    public class GroupEmployee
    {
        public string? GroupId { get; set; }
        public virtual Group Group { get; set; }

        public string? EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
