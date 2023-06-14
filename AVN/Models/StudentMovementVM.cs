using AVN.Model.Entities;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AVN.Models
{
    public class StudentMovementVM : BasicVM<int>
    {
        [Required(ErrorMessage = "Выберите группу")]
        [DisplayName("Из группы")]
        public string? OldGroupId { get; set; }
        public Group? OldGroup { get; set; }

        [Required(ErrorMessage = "Выберите группу")]
        [DisplayName("В группу")]
        public string? NewGroupId { get; set; }
        public Group? NewGroup { get; set; }

        [Required(ErrorMessage = "Выберите дату")]
        [DisplayName("Дата")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? MovementDate { get; set; }

        [Required(ErrorMessage = "Поле не заполнено")]
        [DisplayName("Номер приказа")]
        [RegularExpression(@"^[0-9]{8}$", ErrorMessage = "Номер должен состоять ровно из 8 цифр.")]
        public string? OrderNumber { get; set; }

        [Required(ErrorMessage = "Выберите тип перемещения")]
        [DisplayName("Тип перемещения")]
        public int? MovementTypeId { get; set; }
        public MovementType? MovementType { get; set; }

        [Required(ErrorMessage = "Выберите учебный год")]
        [DisplayName("Учебный год")]
        public int? AcademicYearId { get; set; }
        public AcademicYear? AcademicYear { get; set; }

        [Required]
        public string? StudentId { get; set; }

    }
}
