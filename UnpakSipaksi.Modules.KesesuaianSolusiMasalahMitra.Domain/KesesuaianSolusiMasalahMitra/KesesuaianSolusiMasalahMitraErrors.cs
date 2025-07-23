using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.KesesuaianSolusiMasalahMitra.Domain.KesesuaianSolusiMasalahMitra
{
    public static class KesesuaianSolusiMasalahMitraErrors
    {
        public static Error EmptyData() =>
            Error.NotFound("KesesuaianSolusiMasalahMitra.EmptyData", "data is not found");
        public static Error NotSameValue() =>
            Error.NotFound("KesesuaianSolusiMasalahMitra.NotSameValue", "not the same value in data 'nilai'");
        public static Error UnknownKategoriSkema() =>
            Error.NotFound("KesesuaianSolusiMasalahMitra.UnknownKategoriSkema", "Unknown schema category");
        public static Error InvalidValueNilai() =>
            Error.NotFound("KesesuaianSolusiMasalahMitra.InvalidValueNilai", "Invalid value 'nilai'");
        public static Error NotFound(Guid Id) =>
            Error.NotFound("KesesuaianSolusiMasalahMitra.NotFound", $"Kesesuaian solusi masalah mitra with the identifier {Id} was not found");

    }
}
