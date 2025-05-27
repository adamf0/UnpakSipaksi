using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace UnpakSipaksi.Common.Application
{
    public static class Helper
    {
        public static readonly Regex GuidV4Regex = new(
            @"^[0-9a-f]{8}-[0-9a-f]{4}-4[0-9a-f]{3}-[89ab][0-9a-f]{3}-[0-9a-f]{12}$",
            RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public static bool BeValidGuidV4(string guid)
        {
            return GuidV4Regex.IsMatch(guid);
        }

        public static bool BeAValidDriveLink(string link, string? domain = null)
        {
            if (!Uri.TryCreate(link, UriKind.Absolute, out var uriResult))
                return false;

            bool isHttpOrHttps = uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps;

            if (!isHttpOrHttps)
                return false;

            if (domain != null)
            {
                return uriResult.Host.Equals(domain, StringComparison.OrdinalIgnoreCase);
            }

            return true;
        }

        public static bool BeValidDate(string tanggal) =>
            DateTime.TryParseExact(tanggal, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out _);
    }
}
