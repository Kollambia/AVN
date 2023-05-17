using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AVN.Common.Enums
{
    public enum StudentStatus
    {
        [Display(Name = "Обучается")] Studying = 0,
        [Display(Name = "Отчислен")] Expelled = 1,
        [Display(Name = "Восстановлен")] Restored = 2
    }
}
