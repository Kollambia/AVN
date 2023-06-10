using System.ComponentModel.DataAnnotations;

namespace AVN.Common.Enums
{
    public enum EmployeePosition
    {
        [Display(Name = "Не указан")] NotSet = -1,
        [Display(Name = "Неопределённый")] Undefined = 0,
        [Display(Name = "Профессор")] Professor = 1,
        [Display(Name = "Декан")] Dean = 2,
        [Display(Name = "Доцент")] AssociateProfessor = 3,
        [Display(Name = "Преподователь")] Lecturer = 4
    }
}
