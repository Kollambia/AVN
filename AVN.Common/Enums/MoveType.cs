using System.ComponentModel.DataAnnotations;

namespace AVN.Common.Enums
{
    public enum MoveType
    {
        [Display(Name = "Зачисление")] Enlisted = 1,
        [Display(Name = "Перевод")] Translated = 2,
        [Display(Name = "Архив")] InArchive = 3,
        [Display(Name = "Окончание")] Graduated = 4,
        [Display(Name = "Отчисление")] Expelled = 5,
        [Display(Name = "Восстановление")] Restored = 6,
        [Display(Name = "Академический отпуск")] AcademicLeaved = 7
    }
}
