using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AVN.Common.Enums
{
    public enum OrderType
    {
        [Display(Name = "Зачисление")] Enrollment = 0,
        [Display(Name = "Перевод")] Translation = 1,
        [Display(Name = "Отчисление")] Deduction = 2,
        [Display(Name = "Академический отпуск")] Academicleave = 3,
    }
}
