using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.KesesuaianWaktuRabLuaranFasilitas.Domain.KesesuaianWaktuRabLuaranFasilitas
{
    public static class KesesuaianWaktuRabLuaranFasilitasErrors
    {
        public static Error EmptyData() =>
            Error.NotFound("KesesuaianWaktuRabLuaranFasilitas.EmptyData", "data is not found");
        public static Error NotSameValue() =>
            Error.NotFound("KesesuaianWaktuRabLuaranFasilitas.NotSameValue", "not the same value in data 'nilai'");
        public static Error UnknownKategoriSkema() =>
            Error.NotFound("KesesuaianWaktuRabLuaranFasilitas.UnknownKategoriSkema", "Unknown schema category");
        public static Error NotFound(Guid Id) =>
            Error.NotFound("KesesuaianWaktuRabLuaranFasilitas.NotFound", $"Kesesuaian waktu rab luaran fasilitas with the identifier {Id} was not found");

    }
}
