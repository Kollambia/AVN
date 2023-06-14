using AVN.Common.Enums;
using AVN.Model.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace AVN.Models
{
    public class EmployeeVM : BasicVM<string>
    {
        [Required]
        [DisplayName("Фамилия")]
        [MaxLength(50)]
        public string SName { get; set; }

        [Required]
        [DisplayName("Имя")]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [DisplayName("Отчество")]
        [MaxLength(50)]
        public string PName { get; set; }

        [DateMinimumAge(18, ErrorMessage = "{0} должен быть кем-то в возрасте не менее {1} лет.")]
        [DisplayName("Дата рожд.")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [DisplayName("Электронная почта")]
        [MaxLength(100)]
        public string Email { get; set; }

        [Required]
        [DisplayName("Пол")]
        public Gender Gender { get; set; }

        [Required]
        [DisplayName("Должность")]
        public EmployeePosition Position { get; set; }

        [Required]
        [DisplayName("Адрес")]
        [MaxLength(300)]
        public string Address { get; set; }

        [Required(ErrorMessage = "Поле не заполнено")]
        [DisplayName("Номер телефона")]
        [RegularExpression(@"^0\(\d{3}\)\d{2}-\d{2}-\d{2}$", ErrorMessage = "Неправильный номер телефона.")]
        public string PhoneNumber { get; set; }

        [DisplayName("Кафедра")]
        public int? DepartmentId { get; set; }
        public virtual Department Department { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Подтвердите пароль")]
        [Compare("Password", ErrorMessage = "Пароли должны совпадать")]
        public string ConfirmPassword { get; set; }
    }
}
