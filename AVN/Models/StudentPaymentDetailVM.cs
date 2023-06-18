using AVN.Common.Enums;
using AVN.Model.Entities;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AVN.Models
{
    public class StudentPaymentDetailVM: BasicVM<int>
    {
        [Required(ErrorMessage = "Поле не заполнено")]
        [DisplayName("Оплата")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Оплата должна быть больше нуля.")]
        public decimal? Payment { get; set; }

        [Required(ErrorMessage = "Выберите дату")]
        [DisplayName("Дата оплаты")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? PaymentDate { get; set; }

        [Required(ErrorMessage = "Поле не заполнено")]
        [DisplayName("Номер")]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Номер должен состоять ровно из 10 цифр.")]
        public string? Number { get; set; }

        [Required(ErrorMessage = "Выберите тип оплаты")]
        [DisplayName("Вид оплаты")]
        public PaymentType? PaymentType { get; set; }

        [DisplayName("Целевое назначение")]
        public string SpecialPurpose { get; set; }

        [DisplayName("За какой учебный год")]
        public int? StudentPaymentId { get; set; }
        public StudentPayment? StudentPayment { get; set; }

    }
}
