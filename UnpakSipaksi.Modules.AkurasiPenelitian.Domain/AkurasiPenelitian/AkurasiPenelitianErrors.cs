using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.AkurasiPenelitian.Domain.AkurasiPenelitian
{
    public static class AkurasiPenelitianErrors
    {
        public static Error EmptyData() =>
            Error.NotFound("AkurasiPenelitian.EmptyData", $"data is not found");

        public static Error NotFound(Guid Id) =>
            Error.NotFound("AkurasiPenelitian.NotFound", $"The event with the identifier {Id} was not found");
        
    }
}
