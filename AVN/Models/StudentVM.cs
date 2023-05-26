using AVN.Model.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using AVN.Common.Enums;

namespace AVN.Models
{
    public class StudentVM : BasicVM
    {
        [Required(ErrorMessage = "Поле не заполнено")]
        [DisplayName("ФИО")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Поле должно содержать от 3 до 50 символов.")]
        public string? FullName { get; set; }

        [DateMinimumAge(16, ErrorMessage = "{0} must be someone at least {1} years of age")]
        [DisplayName("Дата рождения")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Выберите форму обучения")]
        [DisplayName("Форма обучения")]
        public FormOfEducation StudingForm { get; set; }

        [Required(ErrorMessage = "Выберите линию обучения")]
        [DisplayName("Линия обучения")]
        public EducationalLine EducationalLine { get; set; }

        [Required(ErrorMessage = "Выберите акалемическую степень")]
        [DisplayName("Академическая степень")]
        public AcademicDegree AcademicDegree { get; set; }

        [Required(ErrorMessage = "Выберите пол")]
        [DisplayName("Пол")]
        public Gender Gender { get; set; }

        [Required(ErrorMessage = "Выберите гражданство")]
        [DisplayName("Гражданство")]
        public Citizenship Citizenship { get; set; }

        [Required(ErrorMessage = "Поле не заполнено")]
        [DisplayName("Адрес")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Поле должно содержать от 5 до 100 символов.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Поле не заполнено")]
        [RegularExpression(@"^(\+996\s?)?(\()?0\d{2}(\))?[-.\s]?[1-9]\d{2}[-.\s]?\d{2}[-.\s]?\d{2}$", ErrorMessage = "Неправильный номер телефона.")]
        public string PhoneNumber { get; set; }

        public int GroupId { get; set; }
        public Group Group { get; set; }
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
