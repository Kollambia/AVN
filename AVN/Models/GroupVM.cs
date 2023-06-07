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
        [DisplayName("Группа")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Поле должно содержать от 3 до 100 символов.")]
        public string? GroupName { get; set; }

        [Required(ErrorMessage = "Выберите курс")]
        [DisplayName("Курс")]
        public Course? Course { get; set; }

        [Required(ErrorMessage = "Выберите дату")]
        [DisplayName("Дата создания")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DateCreate { get; set; }

        [Required(ErrorMessage = "Выберите форму обучения")]
        [DisplayName("Форма обучения")]
        public FormOfEducation? StudingForm { get; set; }

        [Required(ErrorMessage = "Выберите степень")]
        [DisplayName("Степень")]
        public AcademicDegree? AcademicDegree { get; set; }
        
        [Required(ErrorMessage = "Выберите срок обучения")]
        [DisplayName("Срок")]
        public TrainingPeriod? TrainingPeriod { get; set; }

        [Required(ErrorMessage = "Выберите тип группы")]
        [DisplayName("Тип группы")]
        public GroupType? GroupType { get; set; }

        [Required(ErrorMessage = "Выберите уч. год")]
        [DisplayName("Уч. год")]
        public int? AcademicYearId { get; set; }
        public AcademicYear? AcademicYear { get; set; }

        [Required(ErrorMessage = "Выберите Факультет")]
        [DisplayName("Факультет")]
        public int? FacultyId { get; set; }

        [Required(ErrorMessage = "Выберите кафедру")]
        [DisplayName("Кафедра")]
        public int? DepartmentId { get; set; }

        [Required(ErrorMessage = "Выберите направление")]
        [DisplayName("Специальность")]
        public int? DirectionId { get; set; }
        public Direction? Direction { get; set; }
    }
}
