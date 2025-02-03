using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.JumlahKolaboratorPublikasBereputasi.Domain.JumlahKolaboratorPublikasBereputasi
{
    public static class JumlahKolaboratorPublikasBereputasiErrors
    {
        public static Error EmptyData() =>
            Error.NotFound("JumlahKolaboratorPublikasBereputasi.EmptyData", $"data is not found");

        public static Error NotFound(Guid Id) =>
            Error.NotFound("JumlahKolaboratorPublikasBereputasi.NotFound", $"The event with the identifier {Id} was not found");

    }
}
