using AVN.Common.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AVN.Model.Entities
{
    public class Subject : BaseEntity
    {
        [Required]
        [DisplayName("Наименование предмета")]
        public string Title { get; set; }

        [Required]
        [DisplayName("Описание")]
        public string Description { get; set; }

        [Required]
        [DisplayName("Количество часов")]
        public string CreditHours { get; set; }

        [Required]
        [DisplayName("Курс")]
        public Course Course { get; set; }

        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        public ICollection<SubjectEmployee> SubjectEmployees { get; set; }
    }
}
