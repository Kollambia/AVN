using System.ComponentModel.DataAnnotations;

namespace AVN.Common.Enums
{
    public enum EducationalLine
    {
        [Display(Name = "Бюджет")] Budget = 0,
        [Display(Name = "Контракт")] Contract = 1
    }
}
