using AVN.Model.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using AVN.Common.Enums;

namespace AVN.Models
{
    public class StudentVM : BasicVM<string>
    {
        [Required(ErrorMessage = "Поле не заполнено")]
        [DisplayName("Фамилия")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Поле должно содержать от 3 до 50 символов.")]
        public string SName { get; set; }

        [Required(ErrorMessage = "Поле не заполнено")]
        [DisplayName("Имя")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Поле должно содержать от 3 до 50 символов.")]
        public string Name { get; set; }

        [DisplayName("Отчество")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Поле должно содержать от 3 до 50 символов.")]
        public string? PName { get; set; }

        [DateMinimumAge(16, ErrorMessage = "{0} должен быть кем-то в возрасте не менее {1} лет.")]
        [DisplayName("Дата рождения")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DateOfBirth { get; set; }

        [Required(ErrorMessage = "Выберите форму обучения")]
        [DisplayName("Форма обучения")]
        public FormOfEducation? StudingForm { get; set; }

        [Required(ErrorMessage = "Выберите линию обучения")]
        [DisplayName("Линия обучения")]
        public EducationalLine? EducationalLine { get; set; }

        [Required(ErrorMessage = "Выберите академическую степень")]
        [DisplayName("Академическая степень")]
        public AcademicDegree? AcademicDegree { get; set; }

        [Required(ErrorMessage = "Поле не заполнено")] //to do добавить RegularExpression "2022/123"
        [DisplayName("Номер зачетной книжки")]
        public string? GradeBookNumber { get; set; }

        [Required(ErrorMessage = "Выберите статус")]
        [DisplayName("Статус")]
        public StudentStatus? Status { get; set; }

        [Required(ErrorMessage = "Выберите пол")]
        [DisplayName("Пол")]
        public Gender? Gender { get; set; }

        [Required(ErrorMessage = "Выберите гражданство")]
        [DisplayName("Гражданство")]
        public Citizenship? Citizenship { get; set; }

        [Required(ErrorMessage = "Поле не заполнено")]
        [DisplayName("Адрес")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Поле должно содержать от 5 до 100 символов.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Поле не заполнено")]
        [DisplayName("Номер телефона")]
        [RegularExpression(@"^0\(\d{3}\)\d{2}-\d{2}-\d{2}$", ErrorMessage = "Неправильный номер телефона.")]
        public string PhoneNumber { get; set; }

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
        public int? GroupId { get; set; }

        public Group? Group { get; set; }

        //[Required]
        //[DataType(DataType.Password)]
        //[Display(Name = "Password")]
        //public string Password { get; set; }

        //[Required]
        //[DataType(DataType.Password)]
        //[Display(Name = "Confirm password")]
        //[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        //public string ConfirmPassword { get; set; }
    }

    public class DateMinimumAgeAttribute : ValidationAttribute
    {
        public DateMinimumAgeAttribute(int minimumAge)
        {
            MinimumAge = minimumAge;
            ErrorMessage = "{0} должен быть кем-то в возрасте не менее {1} лет.";
        }

        public override bool IsValid(object value)
        {
            DateTime date;
            if ((value != null && DateTime.TryParse(value.ToString(), out date)))
            {
                return date.AddYears(MinimumAge) < DateTime.Now;
            }

            return false;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(ErrorMessageString, name, MinimumAge);
        }

        public int MinimumAge { get; }
    }
}
