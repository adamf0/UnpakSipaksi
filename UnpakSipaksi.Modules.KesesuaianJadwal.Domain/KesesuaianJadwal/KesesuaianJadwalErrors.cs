using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.KesesuaianJadwal.Domain.KesesuaianJadwal
{
    public static class KesesuaianJadwalErrors
    {
        public static Error EmptyData() =>
            Error.NotFound("KesesuaianJadwal.EmptyData", $"data is not found");

        public static Error NotFound(Guid Id) =>
            Error.NotFound("KesesuaianJadwal.NotFound", $"The event with the identifier {Id} was not found");

    }
}
