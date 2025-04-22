using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.Insentif.Presentation
{
    public static class ValueConverter
    {
        /*
         new(StringComparer.OrdinalIgnoreCase)
        {
            { "external", 0 },
            { "internal", 1 }
        }
         */
        public static int TryConvertToInt(Dictionary<string, int> _valueMap, string value)
        {
            if (!_valueMap.TryGetValue(value, out var result))
                throw new ArgumentException($"Unknown value: {value}");

            return result;
        }
        public static int ConvertToInt(Dictionary<string, int> _valueMap, string value, int defaultValue)
        {
            if (!_valueMap.TryGetValue(value, out var result))
                return defaultValue;

            return result;
        }
    }
}
