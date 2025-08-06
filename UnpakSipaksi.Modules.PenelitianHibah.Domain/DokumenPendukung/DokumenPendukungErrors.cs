using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.PenelitianHibah.Domain.DokumenPendukung
{
    public static class DokumenPendukungErrors
    {
        public static Error EmptyData() =>
            Error.NotFound("DokumenPendukung.EmptyData", "data is not found");
        public static Error NotFound(Guid Id) =>
            Error.NotFound("DokumenPendukung.NotFound", $"DokumenPendukung with identifier {Id} not found");
        public static Error NotFoundHibah(Guid Id) =>
            Error.NotFound("DokumenPendukung.NotFoundHibah", $"Penelitian hibah with identifier {Id} not found");
        public static Error InvalidData() =>
            Error.NotFound("DokumenPendukung.InvalidData", "Hibah penelitian is not match existing data");
        public static Error EmptyResource() =>
            Error.NotFound("DokumenPendukung.EmptyResource", "Resource is not found");
        public static Error DuplicateResource() =>
            Error.NotFound("DokumenPendukung.DuplicateResource", "Resource is duplicate source");
        public static Error InvalidLink() =>
            Error.NotFound("DokumenPendukung.InvalidLink", "Invalid link");
    }
}
