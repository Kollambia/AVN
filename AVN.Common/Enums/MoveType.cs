using System.ComponentModel.DataAnnotations;

namespace AVN.Common.Enums
{
    public enum MoveType
    {
        [Display(Name = "Зачисление")] Enlisted = 1,
        [Display(Name = "Перевод")] Translated = 2,
        [Display(Name = "Перевод на следующий курс")] NextCourseTransfer = 3,
        [Display(Name = "Архив")] InArchive = 4,
        [Display(Name = "Окончание")] Graduated = 5,
        [Display(Name = "Отчисление")] Expelled = 6,
        [Display(Name = "Академический отпуск")] AcademicLeaved = 7,
        [Display(Name = "Восстановление из академ.")] RestoredAcademic = 8,
        [Display(Name = "Восстановление из отчисл.")] RestoredExpelled = 9,
        [Display(Name = "Восстановление из закон.")] RestoredGraduared = 10

    }
}
