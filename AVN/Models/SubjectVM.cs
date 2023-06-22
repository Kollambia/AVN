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
        public string? Title { get; set; }

        [Required(ErrorMessage = "Введите описание")]
        [DisplayName("Описание")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Поле должно содержать от 3 до 100 символов.")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Введите количество кредитов")]
        [DisplayName("Кол-во кредитов")]
        public int? CreditCounts { get; set; }

        [Required(ErrorMessage = "Введите количество часов")]
        [DisplayName("Кол-во часов")]
        public int? HoursAmount { get; set; }

        [Required(ErrorMessage = "Выберите номер курса")]
        [DisplayName("Курс")]
        public Course? Course { get; set; }

        [Required(ErrorMessage = "Выберите семестр")]
        [DisplayName("Семестр")]
        public Semestr? Semester { get; set; }

        [Required(ErrorMessage = "Выберите форму контроля")]
        [DisplayName("Форма контроля")]
        public FinalControlForm? ControlForm { get; set; }

        [Required(ErrorMessage = "Выберите преподавателя для предмета")]
        [DisplayName("Преподаватель")]
        public string? EmployeeId { get; set; }
        public Employee? Employee { get; set; }

        [Required(ErrorMessage = "Выберите кафедру для предмета")]
        [DisplayName("Кафедра")]
        public string? DepartmentId { get; set; }
        public Department? Department { get; set; }
    }
}
