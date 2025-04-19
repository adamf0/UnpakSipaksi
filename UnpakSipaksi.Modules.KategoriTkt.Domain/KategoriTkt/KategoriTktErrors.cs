using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.KategoriTkt.Domain.KategoriTkt
{
    public static class KategoriTktErrors
    {
        public static Error EmptyData() =>
            Error.NotFound("KategoriTkt.EmptyData", "data is not found");
        public static Error NotSameValue() =>
            Error.NotFound("KategoriTkt.NotSameValue", "not the same value in data 'nilai'");
        public static Error UnknownKategoriSkema() =>
            Error.NotFound("KategoriTkt.UnknownKategoriSkema", "Unknown schema category");
        public static Error NotFound(Guid Id) =>
            Error.NotFound("KategoriTkt.NotFound", $"Kategori tkt with identifier {Id} not found");

    }
}
