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
            Error.NotFound("InovasiPemecahanMasalah.EmptyData", $"data is not found");

        public static Error NotFound(Guid Id) =>
            Error.NotFound("InovasiPemecahanMasalah.NotFound", $"The event with the identifier {Id} was not found");

    }
}
