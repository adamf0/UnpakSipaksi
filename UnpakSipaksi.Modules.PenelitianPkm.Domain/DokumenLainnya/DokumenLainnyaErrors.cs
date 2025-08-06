using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.PenelitianPkm.Domain.DokumenLainnya
{
    public static class DokumenLainnyaErrors
    {
        public static Error EmptyData() =>
            Error.NotFound("DokumenLainnya.EmptyData", "data is not found");
        public static Error NotFound(Guid Id) =>
            Error.NotFound("DokumenLainnya.NotFound", $"DokumenLainnya with identifier {Id} not found");
        public static Error NotFoundPkm(Guid Id) =>
            Error.NotFound("DokumenLainnya.NotFoundPkm", $"Pkm with identifier {Id} not found");
        public static Error InvalidData() =>
            Error.NotFound("DokumenLainnya.InvalidData", "Hibah penelitian is not match existing data");
        public static Error EmptyResource() =>
            Error.NotFound("DokumenLainnya.EmptyResource", "Resource is not found");
        public static Error DuplicateResource() =>
            Error.NotFound("DokumenLainnya.DuplicateResource", "Resource is duplicate source");
        public static Error InvalidLink() =>
            Error.NotFound("DokumenLainnya.InvalidLink", "Invalid link");
    }
}
