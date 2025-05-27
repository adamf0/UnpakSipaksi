using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.Kategori.Domain.Kategori
{
    public static class KategoriErrors
    {
        public static Error EmptyData() =>
            Error.NotFound("Kategori.EmptyData", "data is not found");
        public static Error NotFound(Guid Id) =>
            Error.NotFound("Kategori.NotFound", $"Kategori with identifier {Id} not found");

    }
}
