using AVN.Model.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace AVN.Models
{
    public class DirectionVM : BasicVM<int>
    {
        [Required(ErrorMessage = "Поле не заполнено")]
        [DisplayName("Название")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Поле должно содержать от 3 до 100 символов.")]
        public string DirectionName { get; set; }

        [DisplayName("Корот. название")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Поле должно содержать от 2 до 50 символов.")]
        public string? DirectionShortName { get; set; }

        [DisplayName("Описание")]
        [StringLength(300, MinimumLength = 3, ErrorMessage = "Поле должно содержать от 3 до 300 символов.")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Поле не заполнено")]
        [DisplayName("Номер направления")]
        [RegularExpression(@"^[0-9]{6}$", ErrorMessage = "Номер должен состоять ровно из 6 цифр.")]
        public int? DirectionNumber { get; set; }

        [Required(ErrorMessage = "Поле не заполнено")]
        [DisplayName("Стоимость кредита")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Стоимость кредита должна быть больше нуля.")]
        public decimal? CreditCost { get; set; }

        [Required(ErrorMessage = "Выберите факультет")]
        [DisplayName("Факультет")]
        public int? FacultyId { get; set; }

        [Required(ErrorMessage = "Выберите кафедру"), ForeignKey("Department")]
        [DisplayName("Кафедра")]
        public int? DepartmentId { get; set; }
        public Department? Department { get; set; }
    }
}
