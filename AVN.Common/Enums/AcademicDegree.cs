using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AVN.Common.Enums
{
    public enum AcademicDegree
    {
        [Display(Name = "Бакалавр")] Bachelor = 0,
        [Display(Name = "Магистр")] Master = 1
    }
}
