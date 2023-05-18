using System.ComponentModel.DataAnnotations;

namespace AVN.Common.Enums
{
    public enum FormOfEducation
    {
        [Display(Name = "Очная")] FullTime = 0,
        [Display(Name = "Заочная")] Сorrespondence = 1
    }
}
