using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using AVN.Model.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AVN.Models
{
    public class DepartmentVM : BasicVM
    {
        [Required(ErrorMessage = "Поле не заполнено")]
        [DisplayName("Название")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Поле должно содержать от 3 до 100 символов.")]
        public string DepartmentName { get; set; }

        [DisplayName("Корот. название")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Поле должно содержать от 2 до 50 символов.")]
        public string? DepartmentShortName { get; set; }

        [Required(ErrorMessage = "Выберите факультет"), ForeignKey("Faculty")]
        [DisplayName("Факультет")]
        public int? FacultyId { get; set; }

        public Faculty? Faculty { get; set; }
    }
}
