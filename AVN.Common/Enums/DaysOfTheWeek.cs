using System.ComponentModel.DataAnnotations;

namespace AVN.Common.Enums
{
    public enum DaysOfTheWeek
    {
        [Display(Name = "Понедельник")] Monday = 1,
        [Display(Name = "Вторник")] Tuesday = 2,
        [Display(Name = "Среда")] Wednesday = 3,
        [Display(Name = "Четверг")] Thursday = 4,
        [Display(Name = "Пятница")] Friday = 5,
        [Display(Name = "Суббота")] Saturday = 6,
        [Display(Name = "Воскресенье")] Sunday = 7
    }
}
