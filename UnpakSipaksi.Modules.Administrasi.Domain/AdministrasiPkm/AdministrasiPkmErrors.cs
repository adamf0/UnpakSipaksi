using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.Administrasi.Domain.AdministrasiPkm
{
    public static class AdministrasiPkmErrors
    {
        public static Error EmptyData() =>
            Error.NotFound("AdministrasiPkm.EmptyData", "data is not found");
        public static Error NotFound(Guid Id) =>
            Error.NotFound("AdministrasiPkm.NotFound", $"Akurasi penelitian with identifier {Id} not found");

    }
}
