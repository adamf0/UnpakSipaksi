using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.KebaruanReferensi.Domain.KebaruanReferensi
{
    public static class KebaruanReferensiErrors
    {
        public static Error EmptyData() =>
            Error.NotFound("KebaruanReferensi.EmptyData", $"data is not found");

        public static Error NotFound(Guid Id) =>
            Error.NotFound("KebaruanReferensi.NotFound", $"The event with the identifier {Id} was not found");

    }
}
