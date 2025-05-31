using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.PenelitianHibah.Domain.DokumenKontrak
{
    public static class DokumenKontrakErrors
    {
        public static Error EmptyData() =>
            Error.NotFound("DokumenKontrak.EmptyData", "data is not found");
        public static Error NotFound(Guid Id) =>
            Error.NotFound("DokumenKontrak.NotFound", $"DokumenKontrak with identifier {Id} not found");
        public static Error NotFoundHibah(Guid Id) =>
            Error.NotFound("DokumenKontrak.NotFoundHibah", $"Penelitian hibah with identifier {Id} not found");
        public static Error EmptyResource() =>
            Error.NotFound("DokumenKontrak.EmptyResource", "Resource is not found");
        public static Error InvalidData() =>
            Error.NotFound("DokumenKontrak.InvalidData", $"Hibah penelitian is not match existing data");
    }
}
