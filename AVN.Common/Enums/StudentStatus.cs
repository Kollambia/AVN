using System.ComponentModel.DataAnnotations;

namespace AVN.Common.Enums
{
    public enum StudentStatus
    {
        [Display(Name = "Зачислен")] Enlisted = 1,
        [Display(Name = "Обучается")] Studying = 2,
        [Display(Name = "Отчислен")] Expelled = 3,
        [Display(Name = "Восстановлен")] Restored = 4
    }
}
