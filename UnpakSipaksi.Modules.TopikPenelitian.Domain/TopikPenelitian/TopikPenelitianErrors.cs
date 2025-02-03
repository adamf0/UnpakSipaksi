using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.TopikPenelitian.Domain.TopikPenelitian
{
    public static class TopikPenelitianErrors
    {
        public static Error EmptyData() =>
            Error.NotFound("TopikPenelitian.EmptyData", $"data is not found");

        public static Error NotFound(Guid Id) =>
            Error.NotFound("TopikPenelitian.NotFound", $"The event with the identifier {Id} was not found");

        public static Error TemaPenelitianNotFound(Guid Id) =>
            Error.NotFound("TopikPenelitian.TemaPenelitianNotFound", $"The Tema Penelitian with the identifier {Id} was not found");

    }
}
