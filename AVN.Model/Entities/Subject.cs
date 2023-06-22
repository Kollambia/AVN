using AVN.Common.Enums;

namespace AVN.Model.Entities
{
    public class Subject : BaseEntity<Subject, int>
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public int CreditCounts { get; set; }
        public int HoursAmount { get; set; }
        public Course Course { get; set; }
        public Semestr Semester { get; set; }
        public FinalControlForm ControlForm { get; set; }
        public int? DepartmentId { get; set; }
        public virtual Department? Department { get; set; }

        public string? EmployeeId { get; set; }
        public virtual Employee? Employee { get; set; }
        public virtual ICollection<Schedule>? Schedule { get; set; }
        public virtual ICollection<GradeBook>? GradeBook { get; set; }

        public Subject()
        {
            Schedule = new List<Schedule>();
            GradeBook = new List<GradeBook>();
        }

    }
}
