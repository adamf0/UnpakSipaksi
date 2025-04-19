using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.InovasiPemecahanMasalah.Domain.InovasiPemecahanMasalah
{
    public static class InovasiPemecahanMasalahErrors
    {
        public static Error EmptyData() =>
            Error.NotFound("InovasiPemecahanMasalah.EmptyData", "data is not found");
        public static Error NotSameValue() =>
            Error.NotFound("InovasiPemecahanMasalah.NotSameValue", "not the same value in data 'nilai'");
        public static Error UnknownKategoriSkema() =>
            Error.NotFound("InovasiPemecahanMasalah.UnknownKategoriSkema", "Unknown schema category");
        public static Error NotFound(Guid Id) =>
            Error.NotFound("InovasiPemecahanMasalah.NotFound", $"Inovasi pemecahan masalah with identifier {Id} not found");

    }
}
