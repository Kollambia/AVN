namespace AVN.Model.Entities
{
    public class Direction : BaseEntity
    {
        public string DirectionName { get; set; }
        public string? DirectionShortName { get; set; }
        public string? Description { get; set; }
        public int DirectionNumber { get; set; }
        public decimal CreditCost { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        public ICollection<Group> Groups { get; set; }
    }
}
