using AVN.Common.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using AVN.Model.Entities;

namespace AVN.Models
{
    public class SubjectVM : BasicVM<int>
    {
        [Required(ErrorMessage = "Введите название предмета")]
        [DisplayName("Название")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Поле должно содержать от 3 до 100 символов.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Введите описание")]
        [DisplayName("Описание")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Поле должно содержать от 3 до 100 символов.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Введите описание")]
        [DisplayName("Количество часов")]
        [StringLength(150, MinimumLength = 1, ErrorMessage = "Поле должно содержать минимум 1 символ")]
        public string CreditHours { get; set; }

        [Required(ErrorMessage = "Выберите номер курса")]
        [DisplayName("Курс")]
        public Course Course { get; set; }

        [Required(ErrorMessage = "Выберите преподавателя для предмета")]
        [DisplayName("Преподаватели")]
        public string EmployeeId { get; set; }
        public Employee Employee { get; set; }

        [Required(ErrorMessage = "Выберите кафедру для предмета")]
        [DisplayName("Кафедры")]
        public string DepartmentId { get; set; }
        public Department Department { get; set; }
    }
}
