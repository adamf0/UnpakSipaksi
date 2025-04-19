using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.TemaPenelitian.Domain.TemaPenelitian
{
    public static class TemaPenelitianErrors
    {
        public static Error EmptyData() =>
            Error.NotFound("TemaPenelitian.EmptyData", "data is not found");

        public static Error NotFound(Guid Id) =>
            Error.NotFound("TemaPenelitian.NotFound", $"Tema penelitian with the identifier {Id} was not found");

        public static Error FokusPenelitianNotFound(Guid Id) =>
            Error.NotFound("TemaPenelitian.FokusPenelitianNotFound", $"The Fokus Penelitian with the identifier {Id} was not found");

    }
}
