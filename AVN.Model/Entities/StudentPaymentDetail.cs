using AVN.Common.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AVN.Model.Entities
{
    public class StudentPaymentDetail: BaseEntity
    {
        [Required]
        [DisplayName("Оплата")]
        public decimal Payment { get; set; }

        [Required]
        [DisplayName("Дата оплаты")]
        public DateTime PaymentDate { get; set; }

        [Required]
        [DisplayName("Номер")]
        public string Number { get; set; }

        [Required]
        [DisplayName("Вид оплаты")]
        public PaymentType PaymentType { get; set; }

        [Required]
        [DisplayName("Целевое назначение")]
        public string SpecialPurpose { get; set; }

        public int StudentPaymentId { get; set; }
        public StudentPayment StudentPayment { get; set; }
    }
}
