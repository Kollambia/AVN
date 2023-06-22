using AVN.Common.Enums;

namespace AVN.Model.Entities
{
    public class GradeBook : BaseEntity<GradeBook, int>
    {
        public Grades Grade { get; set; }
        public int Points { get; set; }
        public DateTime? Date { get; set; }
        public int AcademicYearId { get; set; }
        public virtual AcademicYear? AcademicYear { get; set; }
        public string GroupId { get; set; }
        public virtual Group Group { get; set; }
        public int SubjectId { get; set; }
        public virtual Subject Subject { get; set; }
        public string StudentId { get; set; }
        public virtual Student Student { get; set; }
    }
}
