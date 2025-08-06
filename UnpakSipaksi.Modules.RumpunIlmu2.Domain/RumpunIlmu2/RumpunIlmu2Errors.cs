using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.RumpunIlmu2.Domain.RumpunIlmu2
{
    public static class RumpunIlmu2Errors
    {
        public static Error EmptyData() =>
            Error.NotFound("RumpunIlmu2.EmptyData", "data is not found");
        public static Error NotFound(Guid Id) =>
            Error.NotFound("RumpunIlmu2.NotFound", $"Rumpun Ilmu 2 with the identifier {Id} was not found");
        public static Error UnknownRumpunIlmu1() =>
            Error.NotFound("RumpunIlmu2.UnknownRumpunIlmu1", "Unknown rumpun ilmu 1");
        public static Error RumpunIlmu1NotFound(Guid Id) =>
            Error.NotFound("RumpunIlmu2.RumpunIlmu1NotFound", $"Rumpun ilmu 1 with the identifier {Id} was not found");
    }
}
