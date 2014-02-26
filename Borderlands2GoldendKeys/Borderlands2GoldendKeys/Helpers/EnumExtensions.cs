using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace Borderlands2GoldendKeys
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Gets the DataAnnotation DisplayName attribute for a given enum (for displaying enums values nicely to users)
        /// </summary>
        /// <param name="value">Enum value to get display for</param>
        /// <returns>Pretty version of enum (if there is one)</returns>
        /// <remarks>
        /// Inspired by :
        ///     http://stackoverflow.com/questions/9328972/mvc-net-get-enum-display-name-in-view-without-having-to-refer-to-enum-type-in-vi
        /// </remarks>
        public static string DisplayName(this Enum value)
        {
            Type enumType = value.GetType();
            var enumValue = Enum.GetName(enumType, value);
            MemberInfo member = enumType.GetMember(enumValue)[0];
            string outString = string.Empty;

            var attrs = member.GetCustomAttributes(typeof(DisplayAttribute), false);
            if (attrs.Any())
            {
                var displayAttr = ((DisplayAttribute)attrs[0]);

                outString = displayAttr.Name;

                if (displayAttr.ResourceType != null)
                {
                    outString = displayAttr.GetName();
                }
            }
            else
            {
                outString = value.ToString();
            }

            return outString;
        }
    }
}