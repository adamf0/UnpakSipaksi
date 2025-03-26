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
            Error.NotFound("RumpunIlmu3.EmptyData", $"data is not found");

        public static Error NotFound(Guid Id) =>
            Error.NotFound("RumpunIlmu3.NotFound", $"The event with the identifier {Id} was not found");
        public static Error RumpunIlmu1NotFound(Guid Id) =>
            Error.NotFound("RumpunIlmu3.RumpunIlmu1NotFound", $"The event with the identifier UuidRumpunIlmu2 {Id} was not found");
    }
}
