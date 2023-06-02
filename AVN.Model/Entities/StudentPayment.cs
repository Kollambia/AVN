using AVN.Common.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AVN.Model.Entities
{
    public class StudentPayment: BaseEntity<StudentPayment, int>
    {
        [Required]
        [DisplayName("Учебный год")]
        public int AcademicYear { get; set; } // В формате 2022

        [Required]
        [DisplayName("Контракт")]
        public decimal Contract { get; set; }

        [Required]
        [DisplayName("Оплачено")]
        public decimal Payed { get; set; }

        [Required]
        [DisplayName("Долг")]
        public decimal Debt { get; set; }

        public Course Course { get; set; }

        public int GroupId { get; set; }
        public virtual Group Group { get; set; }

        public string? StudentId { get; set; }
        public virtual Student Student { get; set; }

        public virtual ICollection<StudentPaymentDetail> PaymentDetails { get; set; }

        public StudentPayment()
        {
            PaymentDetails= new List<StudentPaymentDetail>();
        }
    }
}
