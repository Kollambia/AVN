namespace AVN.Model.Entities
{
    public class GroupEmployee
    {
        public int? GroupId { get; set; }
        public virtual Group Group { get; set; }

        public int? EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
