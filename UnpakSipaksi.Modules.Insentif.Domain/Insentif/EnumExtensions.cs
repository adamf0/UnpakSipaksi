using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.Insentif.Domain.Insentif
{
    public static class EnumExtensions
    {
        // Konversi Enum ke String dengan atribut EnumMember
        public static string ToEnumString(this Enum enumValue)
        {
            var field = enumValue.GetType().GetField(enumValue.ToString());
            var attribute = (EnumMemberAttribute)Attribute.GetCustomAttribute(field, typeof(EnumMemberAttribute));
            return attribute != null ? attribute.Value : enumValue.ToString().ToLower();
        }

        // Konversi String ke Enum
        public static T ToEnumFromString<T>(this string stringValue) where T : Enum
        {
            foreach (T enumValue in Enum.GetValues(typeof(T)))
            {
                if (enumValue.ToEnumString().Equals(stringValue, StringComparison.OrdinalIgnoreCase))
                {
                    return enumValue;
                }
            }
            throw new ArgumentException($"Invalid value for {typeof(T).Name}: {stringValue}");
        } //"Co Author".ToEnumFromString<JenisPublikasi>();

        public static T ToEnumFromInt<T>(this int intValue) where T : struct, Enum
        {
            if (Enum.IsDefined(typeof(T), intValue))
            {
                return (T)(object)intValue;
            }
            throw new ArgumentException($"Invalid value for {typeof(T).Name}: {intValue}");
        }

        public static string[] GetAllEnumStrings<T>() where T : Enum
        {
            return Enum.GetValues(typeof(T))
                .Cast<Enum>()
                .Select(e => e.ToEnumString())
                .ToArray();
        } //var allStrings = EnumExtensions.GetAllEnumStrings<JenisPublikasi>();

        public static int[] GetAllEnumValues<T>() where T : Enum
        {
            return Enum.GetValues(typeof(T))
                .Cast<Enum>()
                .Select(e => Convert.ToInt32(e)) // Mengonversi enum ke int
                .ToArray();
        }
    }
}
