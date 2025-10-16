using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace SYSTEM_INGTEGRATION_NEMSU.Client.Helper
{
    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum value)
        {
            return value.GetType()
                .GetMember(value.ToString())
                .First()
                .GetCustomAttribute<DisplayAttribute>()?
                .Name ?? value.ToString();
        }
    }
}
