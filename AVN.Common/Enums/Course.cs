using System.ComponentModel.DataAnnotations;

namespace AVN.Common.Enums
{
    public enum Course
    {
        [Display(Name = "1")] First = 0,
        [Display(Name = "2")] Second = 1,
        [Display(Name = "3")] Third = 2,
        [Display(Name = "4")] Fourth = 3,
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

                default:
                    return string.Empty;
            }
        }
    }
}
