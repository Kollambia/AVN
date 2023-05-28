using System.ComponentModel.DataAnnotations;

namespace AVN.Common.Enums
{
    public enum StudentStatus
    {
        [Display(Name = "Зачислен")] Enlisted = 0,
        [Display(Name = "Обучается")] Studying = 1,
        [Display(Name = "Отчислен")] Expelled = 2,
        [Display(Name = "Восстановлен")] Restored = 3
    }
}
