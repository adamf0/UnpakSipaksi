using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.PenelitianPkm.Domain.Luaran
{
    public static class LuaranErrors
    {
        public static Error EmptyData() =>
            Error.NotFound("Luaran.EmptyData", "data is not found");
        public static Error NotFound(Guid Id) =>
            Error.NotFound("Luaran.NotFound", $"Luaran with identifier {Id} not found");
        public static Error NotFoundHibah(Guid Id) =>
            Error.NotFound("Luaran.NotFoundHibah", $"Penelitian hibah with identifier {Id} not found");
        public static Error UnknownJenisLuaran() =>
            Error.NotFound("IndikatorCapaian.UnknownJenisLuaran", "Unknown jenis luaran");
        public static Error UnknownIndikatorCapaian() =>
            Error.NotFound("IndikatorCapaian.UnknownIndikatorCapaian", "Unknown indikator capaian");
        public static Error InvalidData() =>
            Error.NotFound("Luaran.InvalidData", $"Hibah penelitian is not match existing data");
    }
}
