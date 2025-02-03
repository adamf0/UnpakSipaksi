using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.ArtikelMediaMassa.Domain.ArtikelMediaMassa
{
    public static class ArtikelMediaMassaErrors
    {
        public static Error EmptyData() =>
            Error.NotFound("ArtikelMediaMassa.EmptyData", $"data is not found");

        public static Error NotFound(Guid Id) =>
            Error.NotFound("ArtikelMediaMassa.NotFound", $"The event with the identifier {Id} was not found");

    }
}
