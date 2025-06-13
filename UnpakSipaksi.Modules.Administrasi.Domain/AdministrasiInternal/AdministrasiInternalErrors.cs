using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.Administrasi.Domain.AdministrasiInternal
{
    public static class AdministrasiInternalErrors
    {
        public static Error EmptyData() =>
            Error.NotFound("AdministrasiInternal.EmptyData", "data is not found");
        public static Error InvalidArgument() =>
            Error.NotFound("AdministrasiInternal.InvalidArgument", "data is invalid argument");
        public static Error InvalidKeputusan() =>
            Error.NotFound("AdministrasiInternal.InvalidKeputusan", "invalid keputusan");
        public static Error NotFound(Guid Id) =>
            Error.NotFound("AdministrasiInternal.NotFound", $"Akurasi penelitian with identifier {Id} not found");

    }
}
