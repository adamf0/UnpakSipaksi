using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.KesesuaianPenugasan.Domain.KesesuaianPenugasan
{
    public static class KesesuaianPenugasanErrors
    {
        public static Error EmptyData() =>
            Error.NotFound("KesesuaianPenugasan.EmptyData", "data is not found");
        public static Error NotSameValue() =>
            Error.NotFound("KesesuaianPenugasan.NotSameValue", "not the same value in data 'nilai'");
        public static Error UnknownKategoriSkema() =>
            Error.NotFound("KesesuaianPenugasan.UnknownKategoriSkema", "Unknown schema category");
        public static Error InvalidValueNilai() =>
            Error.NotFound("KesesuaianPenugasan.InvalidValueNilai", "Invalid value 'nilai'");
        public static Error NotFound(Guid Id) =>
            Error.NotFound("KesesuaianPenugasan.NotFound", $"Kesesuaian penugasan with the identifier {Id} was not found");

    }
}
