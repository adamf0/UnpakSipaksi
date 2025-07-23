using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.FokusPenelitian.Domain.FokusPenelitian
{
    public static class FokusPenelitianErrors
    {
        public static Error EmptyData() =>
            Error.NotFound("FokusPenelitian.EmptyData", "data is not found");
        public static Error NotFound(Guid Id) =>
            Error.NotFound("FokusPenelitian.NotFound", $"Fokus penelitian with identifier {Id} not found");

    }
}
