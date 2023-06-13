using AVN.Common.Enums;
using AVN.Model.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace AVN.Models
{
    public class OrderVM : BasicVM<int>
    {
        [Required(ErrorMessage = "Выберите дату")]
        [DisplayName("Дата приказа")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? Date { get; set; }

        [Required(ErrorMessage = "Поле не заполнено")]
        [DisplayName("Номер приказа")]
        [RegularExpression(@"^[0-9]{8}$", ErrorMessage = "Номер должен состоять ровно из 8 цифр.")]
        public string? Number { get; set; }

        [DisplayName("Комментарий")]
        [StringLength(300, MinimumLength = 3, ErrorMessage = "Поле должно содержать от 3 до 300 символов.")]
        public string? Note { get; set; }

        [Required(ErrorMessage = "Выберите тип приказа")]
        [DisplayName("Тип приказа")]
        public int? OrderTypeId { get; set; }

        public Course? Course { get; set; }
        public int? MovementTypeId { get; set; }
        public int? AcademicYearId { get; set; }
        public string? GroupId { get; set; }
        public string? StudentId { get; set; }
    }
}
