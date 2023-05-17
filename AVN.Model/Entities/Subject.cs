using AVN.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        public ICollection<SubjectEmployee> SubjectEmployees { get; set; }
    }
}
