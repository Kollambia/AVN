using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AVN.Common.Enums
{
    public enum Course
    {
        [Display(Name = "Не указан")] NotSet = -1,
        [Display(Name = "Неопределённый")] Undefined = 0,
        [Display(Name = "1")] First = 1,
        [Display(Name = "2")] Second = 2,
        [Display(Name = "3")] Third = 3,
        [Display(Name = "4")] Fourth = 4,
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
