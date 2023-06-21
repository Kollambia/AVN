using System.ComponentModel.DataAnnotations;

namespace AVN.Common.Enums
{
    public enum Grades
    {
        [Display(Name = "н/я")] DontPass = 0,
        [Display(Name = "Отл.")] Great = 1,
        [Display(Name = "Хор.")] Good = 2,
        [Display(Name = "Удов.")] Satisfactorily = 3,
        [Display(Name = "Неудов.")] Unsatisfactory = 4,
        [Display(Name = "Зачет")] Offset = 5,
        [Display(Name = "Незачет")] Fail = 6
    }
}
