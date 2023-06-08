using System.ComponentModel.DataAnnotations;

namespace AVN.Common.Enums
{
    public enum GroupType
    {
        [Display(Name = "Студенты")] Students = 1,
        [Display(Name = "Абитуриенты")] Enrolled = 2,
        [Display(Name = "Архив")] Archived = 3,
        [Display(Name = "Окончившие")] Graduated = 4,
        [Display(Name = "Отчисленные")] Expelled = 5,
        [Display(Name = "Академический отпуск")] AcademicLeaved = 6
    }
}
