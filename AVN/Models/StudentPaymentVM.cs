using AVN.Common.Enums;
using AVN.Model.Entities;
using System.ComponentModel;

namespace AVN.Models
{
    public class StudentPaymentVM: BasicVM<int>
    {
        [DisplayName("Учебный год")]
        public int? AcademicYearId { get; set; } // В формате 2022
        public AcademicYear? AcademicYear { get; set; }

        [DisplayName("Год создания договора")]
        public int? RecruitmentYear { get; set; } // год создания договора

        [DisplayName("Контракт")]
        public decimal? Contract { get; set; }

        [DisplayName("Оплачено")]
        public decimal? Payed { get; set; }

        [DisplayName("Долг")]
        public decimal? Debt { get; set; }

        public string? StudentId { get; set; }
        public Student? Student { get; set; }

        [DisplayName("Курс")]
        public Course? Course { get; set; }

        [DisplayName("Специальность в которую оплатил")]
        public string? GroupId { get; set; }
        public Group? Group { get; set; }

    }
}
