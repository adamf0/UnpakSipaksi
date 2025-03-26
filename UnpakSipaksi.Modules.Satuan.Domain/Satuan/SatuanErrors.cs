using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.Satuan.Domain.Satuan
{
    public static class SatuanErrors
    {
        public static Error EmptyData() =>
            Error.NotFound("Satuan.EmptyData", $"data is not found");

        public static Error NotFound(Guid Id) =>
            Error.NotFound("Satuan.NotFound", $"The event with the identifier {Id} was not found");

    }
}
