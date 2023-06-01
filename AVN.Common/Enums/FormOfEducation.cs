using System.ComponentModel.DataAnnotations;

namespace AVN.Common.Enums
{
    public enum FormOfEducation
    {
        [Display(Name = "Очная")] FullTime = 1,
        [Display(Name = "Заочная")] Сorrespondence = 2
    }
}
