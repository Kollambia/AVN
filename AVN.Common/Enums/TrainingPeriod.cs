using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AVN.Common.Enums
{
    public enum TrainingPeriod
    {
        [Display(Name = "1 год")] OneYear = 1,
        [Display(Name = "2 года")] TwoYear = 2,
        [Display(Name = "3 года")] ThreeYear = 3,
        [Display(Name = "4 года")] FourYear = 4,
        [Display(Name = "5 лет")] SixYear = 5,
        [Display(Name = "6 лет")] SevenYear = 6
    }
}
