using System.ComponentModel.DataAnnotations;

namespace AVN.Common.Enums
{
    public enum AcademicDegree
    {
        [Display(Name = "Бакалавр")] Bachelor = 1,
        [Display(Name = "Магистр")] Master = 2
    }
}
