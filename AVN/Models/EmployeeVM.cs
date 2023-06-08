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

        [Required]
        [DisplayName("Дата рождения")]
        [MaxLength(100)]
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

        [Required]
        [DisplayName("Номер телефона")]
        [MaxLength(30)]
        public string PhoneNumber { get; set; }

        public int? DepartmentId { get; set; }
        public virtual Department Department { get; set; }
    }
}
