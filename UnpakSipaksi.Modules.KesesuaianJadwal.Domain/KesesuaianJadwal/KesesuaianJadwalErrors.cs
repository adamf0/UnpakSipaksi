using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.KesesuaianJadwal.Domain.KesesuaianJadwal
{
    public static class KesesuaianJadwalErrors
    {
        public static Error EmptyData() =>
            Error.NotFound("KesesuaianJadwal.EmptyData", "data is not found");
        public static Error NotSameValue() =>
            Error.NotFound("KesesuaianJadwal.NotSameValue", "not the same value in data 'nilai'");
        public static Error UnknownKategoriSkema() =>
            Error.NotFound("KesesuaianJadwal.UnknownKategoriSkema", "Unknown schema category");
        public static Error NotFound(Guid Id) =>
            Error.NotFound("KesesuaianJadwal.NotFound", $"Kesesuaian jadwal with the identifier {Id} was not found");

    }
}
