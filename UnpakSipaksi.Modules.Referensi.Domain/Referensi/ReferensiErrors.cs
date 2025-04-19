using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.Referensi.Domain.Referensi
{
    public static class ReferensiErrors
    {
        public static Error EmptyData() =>
            Error.NotFound("Referensi.EmptyData", "data is not found");

        public static Error NotFound(Guid Id) =>
            Error.NotFound("Referensi.NotFound", $"Referensi with the identifier {Id} was not found");

        public static Error NotFoundKebaruanReferensiId() =>
            Error.NotFound("Referensi.NotFoundKebaruanReferensiId", $"data kebaruanReferensiId is not found");

        public static Error NotFoundRelevansiKualitasReferensiId() =>
            Error.NotFound("Referensi.NotFoundRelevansiKualitasReferensiId", $"data relevansiKualitasReferensiId is not found");

    }
}
