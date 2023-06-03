using AVN.Common.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AVN.Model.Entities
{
    public class StudentPayment: BaseEntity<StudentPayment, int>
    {
        public decimal Contract { get; set; }
        public decimal Payed { get; set; }
        public decimal Debt { get; set; }
        public Course Course { get; set; }
        public int RecruitmentYear { get; set; } // год создания договора

        public int? AcademicYearId { get; set; }
        public virtual AcademicYear? AcademicYear { get; set; }

        public int GroupId { get; set; }
        public virtual Group? Group { get; set; }

        public string? StudentId { get; set; }
        public virtual Student? Student { get; set; }

        public virtual ICollection<StudentPaymentDetail>? PaymentDetails { get; set; }

        public StudentPayment()
        {
            PaymentDetails= new List<StudentPaymentDetail>();
        }
    }
}
