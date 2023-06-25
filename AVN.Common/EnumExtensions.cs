using System.ComponentModel.DataAnnotations;
using System.Reflection;
using AVN.Common.Enums;

namespace AVN.Common
{
    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum enumValue)
        {
            return enumValue.GetType()
                .GetMember(enumValue.ToString())
                .FirstOrDefault()?
                .GetCustomAttribute<DisplayAttribute>()?
                .Name ?? enumValue.ToString();
        }
        public static string GetRoleName(EmployeePosition position)
        {
            var displayName = position.GetType()
                .GetMember(position.ToString())
                .First()
                .GetCustomAttribute<DisplayAttribute>()
                .GetName();
            return displayName;
        }
    }
}
