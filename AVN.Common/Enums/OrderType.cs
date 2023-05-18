using System.ComponentModel.DataAnnotations;

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
