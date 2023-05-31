using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace AVN.Models
{
    public class FacultyVM : BasicVM<int>
    {
        [Required(ErrorMessage = "Поле не заполнено")]
        [DisplayName("Название")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Поле должно содержать от 3 до 100 символов.")]
        public string FacultyName { get; set; }

        [DisplayName("Корот. название")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Поле должно содержать от 3 до 50 символов.")]
        public string? FacultyShortName { get; set; }

        [Required(ErrorMessage = "Поле не заполнено")]
        [DisplayName("ФИО Декана")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Поле должно содержать от 3 до 50 символов.")]
        public string DeanName { get; set; }
    }
}
