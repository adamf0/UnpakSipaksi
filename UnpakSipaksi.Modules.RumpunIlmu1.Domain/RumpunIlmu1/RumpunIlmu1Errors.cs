using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.RumpunIlmu1.Domain.RumpunIlmu1
{
    public static class RumpunIlmu1Errors
    {
        public static Error EmptyData() =>
            Error.NotFound("RumpunIlmu1.EmptyData", $"data is not found");

        public static Error NotFound(Guid Id) =>
            Error.NotFound("RumpunIlmu1.NotFound", $"The event with the identifier {Id} was not found");

    }
}
