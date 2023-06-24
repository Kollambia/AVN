using System.ComponentModel.DataAnnotations;

namespace AVN.Common.Enums
{
    public enum Semestr
    {
        [Display(Name = "1")] First = 1,
        [Display(Name = "2")] Second = 2,
        [Display(Name = "3")] Third = 3,
        [Display(Name = "4")] Fourth = 4,
        [Display(Name = "5")] Fifth = 5,
        [Display(Name = "6")] Sixth = 6,
        [Display(Name = "7")] Seventh = 7,
        [Display(Name = "8")] Eighth = 8,
        [Display(Name = "9")] Ninth = 9,
        [Display(Name = "10")] Tenth = 10,
        [Display(Name = "11")] Eleventh = 11,
        [Display(Name = "12")] Twelfth = 12
    }

    public static class SemestrExtensions
    {
        public static string GetSemesterInWriting(this Semestr semestr)
        {
            switch (semestr)
            {
                case Semestr.First:
                    return "1-семестр";
                case Semestr.Second:
                    return "2-семестр";
                case Semestr.Third:
                    return "3-семестр";
                case Semestr.Fourth:
                    return "4-семестр";
                case Semestr.Fifth:
                    return "5-семестр";
                case Semestr.Sixth:
                    return "6-семестр";
                case Semestr.Seventh:
                    return "7-семестр";
                case Semestr.Eighth:
                    return "8-семестр";
                case Semestr.Ninth:
                    return "9-семестр";
                case Semestr.Tenth:
                    return "10-семестр";
                case Semestr.Eleventh:
                    return "11-семестр";
                case Semestr.Twelfth:
                    return "12-семестр";
                default:
                    return string.Empty;
            }
        }
    }
}
