using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.KesesuaianSolusiMasalahMitra.Domain.KesesuaianSolusiMasalahMitra
{
    public static class KesesuaianSolusiMasalahMitraErrors
    {
        public static Error EmptyData() =>
            Error.NotFound("KesesuaianSolusiMasalahMitra.EmptyData", $"data is not found");

        public static Error NotFound(Guid Id) =>
            Error.NotFound("KesesuaianSolusiMasalahMitra.NotFound", $"The event with the identifier {Id} was not found");

    }
}
