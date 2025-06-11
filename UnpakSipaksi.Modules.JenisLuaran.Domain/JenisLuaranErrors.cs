using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.JenisLuaran.Domain
{
    public static class JenisLuaranErrors
    {
        public static Error EmptyData() =>
            Error.NotFound("JenisLuaran.EmptyData", "data is not found");
        public static Error NotSameValue() =>
            Error.NotFound("JenisLuaran.NotSameValue", "not the same value in data 'nilai'");
        public static Error UnknownKategoriSkema() =>
            Error.NotFound("JenisLuaran.UnknownKategoriSkema", "Unknown schema category");
        public static Error NotFound(Guid Id) =>
            Error.NotFound("JenisLuaran.NotFound", $"Jenis luaran with identifier {Id} not found");

    }
}
