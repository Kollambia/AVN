using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AVN.Model.Entities;

public class Student : BaseEntity
{
    [Required]
    [DisplayName("ФИО")]
    [MaxLength(100)]
    public string FullName { get; set; }

    [Required]
    [DisplayName("Группа")]
    [MaxLength(50)]
    public string Group { get; set; }

    [Required]
    [DisplayName("Статус")]
    [MaxLength(500)]
    public string Status { get; set; }

    [Required]
    [DisplayName("Дата рождения")]
    [MaxLength(100)]
    public DateTime DateOfBirth { get; set; }

    [Required]
    [DisplayName("Форма обучения")]
    public string StudingForm { get; set; }

    [MaxLength(100)]
    [Required]
    [DisplayName("Линия обучения")]
    public string EducationalLine { get; set; }

    [MaxLength(100)]
    [Required]
    [DisplayName("Номер зачетной книжки")]
    public string GradeBookNumber { get; set; }

    [MaxLength(20)]
    [Required]
    [DisplayName("Пол")]
    public string Gender { get; set; }

    [MaxLength(100)]
    [Required]
    [DisplayName("Гражданство")]
    public string Citizenship { get; set; }

    [MaxLength(300)]
    [Required]
    [DisplayName("Адрес")]
    public string Address { get; set; }

    [MaxLength(30)]
    [Required]
    [DisplayName("Номер телефона")]
    public string PhoneNumber { get; set; }

    [MaxLength(1000)]
    [Required]
    [DisplayName("Приказы")]
    public string Orders { get; set; }

}