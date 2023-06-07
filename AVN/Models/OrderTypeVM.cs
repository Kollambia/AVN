using AVN.Model.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace AVN.Models
{
    public class OrderTypeVM : BasicVM<int>
    {
        [Required(ErrorMessage = "Поле не заполнено")]
        [DisplayName("Название")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Поле должно содержать от 3 до 100 символов.")]
        public string? Name { get; set; }


        [Required(ErrorMessage = "Выберите тип перемещения")]
        [DisplayName("Тип перемещения")]
        public int? MovementTypeId { get; set; }

        public MovementType? MovementType { get; set; }
    }
}
