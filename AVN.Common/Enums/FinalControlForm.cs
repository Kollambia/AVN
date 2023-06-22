using System.ComponentModel.DataAnnotations;

namespace AVN.Common.Enums
{
    public enum FinalControlForm
    {
        [Display(Name = "Экзамен")] Exam = 1,
        [Display(Name = "Зачет")] Ofset = 2,
        [Display(Name = "Курc/пр")] Practic = 3
    }
}
