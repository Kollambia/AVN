using AVN.Common.Enums;
using AVN.Model.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace AVN.Models
{
    public class GroupVM : BasicVM<int>
    {
        [Required(ErrorMessage = "Поле не заполнено")]
        [DisplayName("Название")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Поле должно содержать от 3 до 100 символов.")]
        public string? GroupName { get; set; }

        [Required(ErrorMessage = "Выберите форму обучения")]
        [DisplayName("Курс")]
        public Course? Course { get; set; }

        [DisplayName("Дата создания")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DateCreate { get; set; }

        [Required(ErrorMessage = "Выберите Факультет")]
        [DisplayName("Факультет")]
        public int? FacultyId { get; set; }

        [Required(ErrorMessage = "Выберите кафедру")]
        [DisplayName("Кафедра")]
        public int? DepartmentId { get; set; }

        [Required(ErrorMessage = "Выберите направление"), ForeignKey("Direction")]
        [DisplayName("Направление")]
        public int? DirectionId { get; set; }
        public Direction? Direction { get; set; }
    }
}
