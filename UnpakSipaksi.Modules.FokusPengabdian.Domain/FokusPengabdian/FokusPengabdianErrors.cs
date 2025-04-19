using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.FokusPengabdian.Domain.FokusPengabdian
{
    public static class FokusPengabdianErrors
    {
        public static Error EmptyData() =>
            Error.NotFound("FokusPengabdian.EmptyData", "data is not found");

        public static Error NotFound(Guid Id) =>
            Error.NotFound("FokusPengabdian.NotFound", $"Fokus pengabdian with identifier {Id} not found");

    }
}
