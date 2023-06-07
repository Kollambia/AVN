using System.ComponentModel.DataAnnotations;

namespace AVN.Common.Enums
{
    public enum GroupType
    {
        [Display(Name = "Студенты")] Students = 1,
        [Display(Name = "Архив")] Archived = 2,
        [Display(Name = "Окончившие")] Graduated = 3,
        [Display(Name = "Отчисленные")] Expelled = 4,
        [Display(Name = "Академический отпуск")] AcademicLeaved = 5
    }
}
