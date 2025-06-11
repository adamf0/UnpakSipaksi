using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.IndikatorCapaian.Domain
{
    public static class IndikatorCapaianErrors
    {
        public static Error EmptyData() =>
            Error.NotFound("IndikatorCapaian.EmptyData", "data is not found");
        public static Error NotSameValue() =>
            Error.NotFound("IndikatorCapaian.NotSameValue", "not the same value in data 'nilai'");
        public static Error UnknownJenisLuaran() =>
            Error.NotFound("IndikatorCapaian.UnknownJenisLuaran", "Unknown jenis luaran");
        public static Error NotFound(Guid Id) =>
            Error.NotFound("IndikatorCapaian.NotFound", $"Indikator capaian with identifier {Id} not found");

    }
}
