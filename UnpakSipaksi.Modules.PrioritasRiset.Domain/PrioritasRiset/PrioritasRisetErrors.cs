using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.PrioritasRiset.Domain.PrioritasRiset
{
    public static class PrioritasRisetErrors
    {
        public static Error EmptyData() =>
            Error.NotFound("PrioritasRiset.EmptyData", "data is not found");

        public static Error NotFound(Guid Id) =>
            Error.NotFound("PrioritasRiset.NotFound", $"Prioritas riset with the identifier {Id} was not found");

    }
}
