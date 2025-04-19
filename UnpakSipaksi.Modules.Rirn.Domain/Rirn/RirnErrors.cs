using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.Rirn.Domain.Rirn
{
    public static class RirnErrors
    {
        public static Error EmptyData() =>
            Error.NotFound("Rirn.EmptyData", "data is not found");

        public static Error NotFound(Guid Id) =>
            Error.NotFound("Rirn.NotFound", $"Rirn with the identifier {Id} was not found");

    }
}
