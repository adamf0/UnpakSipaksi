using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.KategoriMitraPenelitian.Domain.KategoriMitraPenelitian
{
    public static class KategoriMitraPenelitianErrors
    {
        public static Error EmptyData() =>
            Error.NotFound("KategoriMitraPenelitian.EmptyData", $"data is not found");

        public static Error NotFound(Guid Id) =>
            Error.NotFound("KategoriMitraPenelitian.NotFound", $"The event with the identifier {Id} was not found");

    }
}
