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
                    return "Жен.";

                case Gender.Male:
                    return "Муж.";

                case Gender.Undefined:
                    return "Не определено";

                default:
                    return string.Empty;
            }
        }
    }
}
