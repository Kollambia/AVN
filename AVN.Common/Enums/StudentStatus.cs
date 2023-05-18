using System.ComponentModel.DataAnnotations;

namespace AVN.Common.Enums
{
    public enum StudentStatus
    {
        [Display(Name = "Обучается")] Studying = 0,
        [Display(Name = "Отчислен")] Expelled = 1,
        [Display(Name = "Восстановлен")] Restored = 2
    }
}
