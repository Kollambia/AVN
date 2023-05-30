using AVN.Common.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AVN.Model.Entities
{
    public class Subject : BaseEntity<Subject, int>
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

        public int? DepartmentId { get; set; }
        public virtual Department Department { get; set; }

        public virtual ICollection<SubjectEmployee> SubjectEmployees { get; set; }

        public Subject() 
        { 
            SubjectEmployees= new List<SubjectEmployee>();
        }
    }
}
