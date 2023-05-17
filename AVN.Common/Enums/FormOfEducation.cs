using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AVN.Common.Enums
{
    public enum FormOfEducation
    {
        [Display(Name = "Очная")] FullTime = 0,
        [Display(Name = "Заочная")] Сorrespondence = 1
    }
}
