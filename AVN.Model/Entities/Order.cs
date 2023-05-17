using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AVN.Common.Enums;

namespace AVN.Model.Entities
{
    public class Order : BaseEntity
    {
        [Required]
        [DisplayName("Вид приказа")]
        public OrderType OrderType { get; set; }

        [Required]
        [DisplayName("Дата")]
        public DateTime Date { get; set; }

        [Required]
        [DisplayName("Номер приказа")]
        public string Number { get; set; }

        [Required]
        [DisplayName("Примечание")]
        public string? Note { get; set; }

    }
}
