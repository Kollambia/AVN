using System.ComponentModel.DataAnnotations;

namespace AVN.Common.Enums
{
    public enum EducationalLine
    {
        [Display(Name = "Бюджет")] Budget = 1,
        [Display(Name = "Контракт")] Contract = 2
    }
}
