using System.ComponentModel;

namespace AVN.Models
{
    public class StudentViaFilterVM
    {
        [DisplayName("Факультет")]
        public int? FacultyId { get; set; }

        [DisplayName("Кафедра")]
        public int? DepartmentId { get; set; }

        [DisplayName("Направление")]
        public int? DirectionId { get; set; }

        [DisplayName("Группа")]
        public int? GroupId { get; set; }

        public List<StudentVM>? studentVMs { get; set;}
    }
}
