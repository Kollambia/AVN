using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AVN.Common.Enums
{
    public enum EducationalLine
    {
        [Display(Name = "Бюджет")] Budget = 0,
        [Display(Name = "Контракт")] Contract = 1
    }
}
