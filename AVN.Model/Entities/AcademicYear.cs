namespace AVN.Model.Entities
{
    public class AcademicYear : BaseEntity<AcademicYear, int>
    {
        public int YearFrom { get; set; }
        public int YearTo { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Group>? Groups { get; set; }
        public virtual ICollection<StudentMovement>? StudentMovements { get; set; }
        public virtual ICollection<StudentPayment>? StudentPayments { get; set; }
        public virtual ICollection<Order>? Orders { get; set; }
        public virtual ICollection<Schedule>? Schedules { get; set; }

        public AcademicYear()
        {
            Groups = new List<Group>();
            StudentMovements = new List<StudentMovement>();
            StudentPayments = new List<StudentPayment>();
            Orders = new List<Order>();
            Schedules = new List<Schedule>();
        }
    }
}
