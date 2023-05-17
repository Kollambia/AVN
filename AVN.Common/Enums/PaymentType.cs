using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVN.Common.Enums
{
    public enum PaymentType
    {
        [Display(Name = "Наличный расчет")] Cash = 0
    }

}
