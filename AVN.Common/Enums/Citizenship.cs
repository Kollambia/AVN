using System.ComponentModel.DataAnnotations;

namespace AVN.Common.Enums
{
    // to do Доработать, так не годится
    public enum Citizenship
    {
        [Display(Name = "Неопределённый")] Undefined = 1,
        [Display(Name = "Кыргызстан")] Kyrgyzstan = 2,
        [Display(Name = "Казахстан")] Kazakhstan = 3,
        [Display(Name = "Россия")] Russia = 4,
        [Display(Name = "Узбекистан")] Uzbekistan = 5
    }
}
