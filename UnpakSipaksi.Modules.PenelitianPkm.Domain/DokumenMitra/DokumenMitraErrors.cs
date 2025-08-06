using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.PenelitianPkm.Domain.DokumenMitra
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
        public static Error InvalidMitra() =>
            Error.NotFound("DokumenMitra.InvalidMitra", "Mitra name is required and must be at least 2 characters.");

        public static Error InvalidProvinsi() =>
            Error.NotFound("DokumenMitra.InvalidProvinsi", "Provinsi is required.");

        public static Error InvalidKota() =>
            Error.NotFound("DokumenMitra.InvalidKota", "Kota is required.");

        public static Error InvalidPemimpinMitra() =>
            Error.NotFound("DokumenMitra.InvalidPemimpinMitra", "Pemimpin Mitra is required and must be at least 2 characters.");
    }
}
