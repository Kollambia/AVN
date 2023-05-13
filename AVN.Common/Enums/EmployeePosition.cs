using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AVN.Common.Enums
{
    public enum EmployeePosition
    {
        [Display(Name = "Не указан")] NotSet = -1,
        [Display(Name = "Неопределённый")] Undefined = 0,
        [Display(Name = "Администратор")] Administrator = 1,
        [Display(Name = "Профессор")] Professor = 2,
        [Display(Name = "Профессор")] Dean = 3,
        [Display(Name = "Доцент")] AssociateProfessor = 4,
        [Display(Name = "Преподователь")] Lecturer = 5
    }
}
