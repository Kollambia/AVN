using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AVN.Models
{
    public class MovementTypeVM : BasicVM<int>
    {
        [Required(ErrorMessage = "Поле не заполнено")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Поле должно содержать от 3 до 100 символов.")]
        [DisplayName("Тип перемещения")]
        public string? Name { get; set; }
    }
}
