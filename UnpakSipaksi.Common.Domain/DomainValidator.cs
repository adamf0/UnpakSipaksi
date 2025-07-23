using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Common.Domain
{
    public static class DomainValidator
    {
        public static bool IsValidGoogleDriveUrl(string url, string target = "drive.google.com")
        {
            if (!Uri.TryCreate(url, UriKind.Absolute, out var uri))
                return false;

            //if (uri.Scheme != Uri.UriSchemeHttps)
            //    return false;

            if (!string.Equals(uri.Host, target, StringComparison.OrdinalIgnoreCase))
                return false;

            return true;
        }
    }
}
