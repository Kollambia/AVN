using System.ComponentModel.DataAnnotations;

namespace AVN.Common.Enums
{
    public enum TrainingPeriod
    {
        [Display(Name = "1 год")] OneYear = 1,
        [Display(Name = "2 года")] TwoYear = 2,
        [Display(Name = "3 года")] ThreeYear = 3,
        [Display(Name = "4 года")] FourYear = 4,
        [Display(Name = "5 лет")] SixYear = 5,
        [Display(Name = "6 лет")] SevenYear = 6
    }
}
