using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.KategoriSumberDana.Domain.KategoriSumberDana
{
    public static class KategoriSumberDanaErrors
    {
        public static Error EmptyData() =>
            Error.NotFound("KategoriSumberDana.EmptyData", $"data is not found");

        public static Error NotFound(Guid Id) =>
            Error.NotFound("KategoriSumberDana.NotFound", $"The event with the identifier {Id} was not found");

    }
}
