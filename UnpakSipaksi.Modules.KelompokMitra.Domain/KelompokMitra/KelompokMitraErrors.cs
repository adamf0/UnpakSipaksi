using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.KelompokMitra.Domain.KelompokMitra
{
    public static class KelompokMitraErrors
    {
        public static Error EmptyData() =>
            Error.NotFound("KelompokMitra.EmptyData", $"data is not found");

        public static Error NotFound(Guid Id) =>
            Error.NotFound("KelompokMitra.NotFound", $"The event with the identifier {Id} was not found");

    }
}
