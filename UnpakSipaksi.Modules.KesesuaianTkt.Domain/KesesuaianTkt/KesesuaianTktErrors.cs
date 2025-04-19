using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.KesesuaianTkt.Domain.KesesuaianTkt
{
    public static class KesesuaianTktErrors
    {
        public static Error EmptyData() =>
            Error.NotFound("KesesuaianTkt.EmptyData", "data is not found");
        public static Error NotSameValue() =>
           Error.NotFound("KesesuaianTkt.NotSameValue", "not the same value in data 'nilai'");
        public static Error UnknownKategoriSkema() =>
            Error.NotFound("KesesuaianTkt.UnknownKategoriSkema", "Unknown schema category");
        public static Error NotFound(Guid Id) =>
            Error.NotFound("KesesuaianTkt.NotFound", $"Kesesuaian tkt with the identifier {Id} was not found");

    }
}
