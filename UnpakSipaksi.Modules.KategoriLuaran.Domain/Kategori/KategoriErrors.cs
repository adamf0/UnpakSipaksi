using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.KategoriLuaran.Domain.Kategori
{
    public static class KategoriLuaranErrors
    {
        public static Error EmptyData() =>
            Error.NotFound("KategoriLuaran.EmptyData", "data is not found");
        public static Error KategoriNotFound() =>
            Error.NotFound("KategoriLuaran.KategoriNotFound", "Kategori is not found");
        public static Error NotFound(Guid Id) =>
            Error.NotFound("KategoriLuaran.NotFound", $"KategoriLuaran with identifier {Id} not found");

    }
}
