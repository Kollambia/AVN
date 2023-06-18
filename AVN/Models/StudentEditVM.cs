using AVN.Common.Enums;
using AVN.Model.Entities;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AVN.Models
{
    public class StudentEditVM : BasicVM<string>
    {
        [Required(ErrorMessage = "Поле не заполнено")]
        [DisplayName("Фамилия")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Поле должно содержать от 3 до 50 символов.")]
        public string? SName { get; set; }

        [Required(ErrorMessage = "Поле не заполнено")]
        [DisplayName("Имя")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Поле должно содержать от 3 до 50 символов.")]
        public string? Name { get; set; }

        [DisplayName("Отчество")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Поле должно содержать от 3 до 50 символов.")]
        public string? PName { get; set; }

        [Required(ErrorMessage = "Выберите статус")]
        [DisplayName("Статус")]
        public StudentStatus? Status { get; set; }

        [DateMinimumAge(16, ErrorMessage = "{0} должен быть кем-то в возрасте не менее {1} лет.")]
        [DisplayName("Дата рожд.")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DateOfBirth { get; set; }

        [Required(ErrorMessage = "Выберите линию обучения")]
        [DisplayName("Линия обучения")]
        public EducationalLine? EducationalLine { get; set; }

        [Required(ErrorMessage = "Поле не заполнено")]
        [DisplayName("Номер зач. книж.")]
        public string? GradeBookNumber { get; set; }

        [Required(ErrorMessage = "Выберите пол")]
        [DisplayName("Пол")]
        public Gender? Gender { get; set; }

        [Required(ErrorMessage = "Выберите гражданство")]
        [DisplayName("Гражданство")]
        public Citizenship? Citizenship { get; set; }

        [Required(ErrorMessage = "Поле не заполнено")]
        [DisplayName("Адрес")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Поле должно содержать от 5 до 100 символов.")]
        public string? Address { get; set; }

        [Required(ErrorMessage = "Поле не заполнено")]
        [DisplayName("Номер телефона")]
        [RegularExpression(@"^0\(\d{3}\)\d{2}-\d{2}-\d{2}$", ErrorMessage = "Неправильный номер телефона.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Поле не заполнено")]
        [DisplayName("Год набора")]
        [YearRange(2000, ErrorMessage = "Пожалуйста, введите действительный год между 2000 и текущим годом")]
        public int RecruitmentYear { get; set; }

        [DisplayName("Долг")]
        public bool IsHasDebt { get; set; }

        [Required(ErrorMessage = "Выберите факультет")]
        [DisplayName("Факультет")]
        public int? FacultyId { get; set; }

        [Required(ErrorMessage = "Выберите кафедру")]
        [DisplayName("Кафедра")]
        public int? DepartmentId { get; set; }

        [Required(ErrorMessage = "Выберите направление")]
        [DisplayName("Направление")]
        public int? DirectionId { get; set; }

        [Required(ErrorMessage = "Выберите группу")]
        [DisplayName("Группа")]
        public string? GroupId { get; set; }
        public Group? Group { get; set; }

        //to do вынести это в отдельную форму для редактирования
        //[Required(ErrorMessage = "Введите логин")]
        //[Display(Name = "Логин")]
        //[StringLength(15, MinimumLength = 5, ErrorMessage = "Поле должно содержать от 5 до 15 символов.")]
        //public string? Login { get; set; }

        //[Required(ErrorMessage = "Введите пароль")]
        //[DataType(DataType.Password)]
        //[Display(Name = "Пароль")]
        //public string? Password { get; set; }

        //[Required(ErrorMessage = "Введите подтверждение пароля")]
        //[DataType(DataType.Password)]
        //[Display(Name = "Подтвердите пароль")]
        //[Compare("Password", ErrorMessage = "Пароль и подтверждения пароля не совпадают.")]
        //public string? ConfirmPassword { get; set; }

    }


    
}
