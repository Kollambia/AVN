using System.ComponentModel.DataAnnotations;

namespace AVN.Common.Enums
{
    public enum Gender
    {
        [Display(Name = "Не указан")] NotSet = -1,
        [Display(Name = "Женский")] Female = 0,
        [Display(Name = "Мужской")] Male = 1,
        [Display(Name = "Неопределённый")] Undefined = 2
    }

    public static class GenderExtensions
    {
        public static string GetShortName(this Gender sex)
        {
            switch (sex)
            {
                case Gender.Female:
                    return "Ж";

                case Gender.Male:
                    return "М";

                default:
                    return string.Empty;
            }
        }
    }
}
