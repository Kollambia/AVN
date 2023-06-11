using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AVN.Models.FilterVM
{
    public class StudentsFilterVM
    {
        [DisplayName("Факультет")]
        public int? FacultyId { get; set; }

        [DisplayName("Кафедра")]
        public int? DepartmentId { get; set; }

        [DisplayName("Направление")]
        public int? DirectionId { get; set; }

        [DisplayName("Группа")]
        public string? GroupId { get; set; }
    }
}
