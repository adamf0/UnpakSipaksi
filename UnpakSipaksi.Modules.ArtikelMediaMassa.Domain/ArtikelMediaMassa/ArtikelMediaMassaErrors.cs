using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.ArtikelMediaMassa.Domain.ArtikelMediaMassa
{
    public static class ArtikelMediaMassaErrors
    {
        public static Error EmptyData() =>
            Error.NotFound("ArtikelMediaMassa.EmptyData", "data is not found");
        public static Error NotSameValue() =>
            Error.NotFound("ArtikelMediaMassa.NotSameValue", "not the same value in data 'nilai'");
        public static Error UnknownKategoriSkema() =>
            Error.NotFound("ArtikelMediaMassa.UnknownKategoriSkema", "Unknown schema category");
        public static Error InvalidNilai() =>
            Error.NotFound("ArtikelMediaMassa.InvalidNilai", "Nilai is invalid format");
        public static Error NotFound(Guid Id) =>
            Error.NotFound("ArtikelMediaMassa.NotFound", $"Artikel media massa with identifier {Id} not found");

    }
}
