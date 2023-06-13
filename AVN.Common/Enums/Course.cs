using System.ComponentModel.DataAnnotations;

namespace AVN.Common.Enums
{
    public enum Course
    {
        [Display(Name = "1")] First = 1,
        [Display(Name = "2")] Second = 2,
        [Display(Name = "3")] Third = 3,
        [Display(Name = "4")] Fourth = 4,
        [Display(Name = "5")] Fifth = 5,
        [Display(Name = "6")] Sixth = 6
    }

    public static class CourseExtensions
    {
        public static string GetCourseInWriting(this Course course)
        {
            switch (course)
            {
                case Course.First:
                    return "1-курс";

                case Course.Second:
                    return "2-курс";

                case Course.Third:
                    return "3-курс";

                case Course.Fourth:
                    return "4-курс";

                case Course.Fifth:
                    return "5-курс";

                case Course.Sixth:
                    return "6-курс";

                default:
                    return string.Empty;
            }
        }
    }
}
