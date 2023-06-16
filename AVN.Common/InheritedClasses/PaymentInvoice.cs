using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVN.Common.Customs
{
    public class PaymentInvoice
    {
        public string? Faculty { get; set; }
        public string? Department { get; set; }
        public string? Direction { get; set; }
        public string? Group { get; set; }
        public string? Course { get; set; }
        public string? FullName { get; set; }
        public string? EducationForm { get; set; }
        public string? AcademicDegree { get; set; }
        public string? PaymentAccountNumber { get; set; }
        public decimal PaymentAmount { get; set; }
        public string? PaymentPurpose { get; set; }
    }
}
