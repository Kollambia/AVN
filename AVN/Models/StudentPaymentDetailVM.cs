using AVN.Common.Enums;
using AVN.Model.Entities;
using System.ComponentModel;

namespace AVN.Models
{
    public class StudentPaymentDetailVM: BasicVM<int>
    {
        [DisplayName("Оплата")]
        public decimal Payment { get; set; }

        [DisplayName("Дата оплаты")]
        public DateTime PaymentDate { get; set; }

        [DisplayName("Номер")]
        public string Number { get; set; }

        [DisplayName("Вид оплаты")]
        public PaymentType PaymentType { get; set; }

        [DisplayName("Целевое назначение")]
        public string SpecialPurpose { get; set; }

        [DisplayName("За какой учебный год")]
        public int? StudentPaymentId { get; set; }
        public StudentPayment? StudentPayment { get; set; }

    }
}
