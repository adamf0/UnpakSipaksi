using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.PenelitianHibah.Domain.DokumenMitra
{
    public static class DokumenMitraErrors
    {
        public static Error EmptyData() =>
            Error.NotFound("DokumenMitra.EmptyData", "data is not found");
        public static Error NotFound(Guid Id) =>
            Error.NotFound("DokumenMitra.NotFound", $"DokumenMitra with identifier {Id} not found");
        public static Error NotFoundPkm(Guid Id) =>
            Error.NotFound("DokumenMitra.NotFoundPkm", $"Pkm with identifier {Id} not found");
        public static Error InvalidData() =>
            Error.NotFound("DokumenMitra.InvalidData", "Hibah penelitian is not match existing data");
        public static Error EmptyResource() =>
            Error.NotFound("DokumenMitra.EmptyResource", "Resource is not found");
        public static Error InvalidKelompokMitra() =>
            Error.NotFound("PenelitianPkm.InvalidKelompokMitra", "Don't recognize kelompok mitra value");
    }
}
