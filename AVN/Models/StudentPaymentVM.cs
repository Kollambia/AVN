using AVN.Common.Enums;
using AVN.Model.Entities;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AVN.Models
{
    public class StudentPaymentVM: BasicVM<int>
    {
        [DisplayName("Учебный год")]
        public int? AcademicYear { get; set; } // В формате 2022

        [DisplayName("Контракт")]
        public decimal? Contract { get; set; }

        [DisplayName("Оплачено")]
        public decimal? Payed { get; set; }

        [DisplayName("Долг")]
        public decimal? Debt { get; set; }

        [DisplayName("Специальность в которую оплатил")]
        public string? StudentId { get; set; }
        public Student? Student { get; set; }

        [DisplayName("Курс")]
        public Course Course { get; set; }

        [DisplayName("Группа")]
        public int GroupId { get; set; }
        public Group? Group { get; set; }

    }
}
