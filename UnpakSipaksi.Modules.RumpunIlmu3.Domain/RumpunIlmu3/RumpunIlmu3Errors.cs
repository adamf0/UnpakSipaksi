using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.RumpunIlmu3.Domain.RumpunIlmu3
{
    public static class RumpunIlmu3Errors
    {
        public static Error EmptyData() =>
            Error.NotFound("RumpunIlmu3.EmptyData", "data is not found");
        public static Error NotFound(Guid Id) =>
            Error.NotFound("RumpunIlmu3.NotFound", $"Rumpun Ilmu 3 with the identifier {Id} was not found");
        public static Error UnknownRumpunIlmu2() =>
            Error.NotFound("RumpunIlmu3.UnknownRumpunIlmu2", "Unknown rumpun ilmu 2");
        public static Error RumpunIlmu2NotFound(Guid Id) =>
            Error.NotFound("RumpunIlmu3.RumpunIlmu2NotFound", $"Rumpun ilmu 2 with the identifier {Id} was not found");
    }
}
