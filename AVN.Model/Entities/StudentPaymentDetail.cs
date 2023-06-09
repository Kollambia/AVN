﻿using AVN.Common.Enums;

namespace AVN.Model.Entities
{
    public class StudentPaymentDetail: BaseEntity<StudentPaymentDetail, int>
    {
        public decimal? Payment { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string Number { get; set; }
        public PaymentType? PaymentType { get; set; }
        public string? SpecialPurpose { get; set; }

        public int? StudentPaymentId { get; set; }
        public virtual StudentPayment StudentPayment { get; set; }
    }
}
