using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.PenelitianPkm.Domain.RAB
{
    public static class RABErrors
    {
        public static Error EmptyData() =>
            Error.NotFound("RAB.EmptyData", "data is not found");
        public static Error NotFound(Guid Id) =>
            Error.NotFound("RAB.NotFound", $"RAB with identifier {Id} not found");
        public static Error InvalidTotal() =>
            Error.NotFound("RAB.InvalidTotal", $"Total value does not match calculation");
        public static Error NotFoundHibah(Guid Id) =>
            Error.NotFound("RAB.NotFoundHibah", $"Penelitian hibah with identifier {Id} not found");
        public static Error InvalidData() =>
            Error.NotFound("RAB.InvalidData", $"Hibah penelitian is not match existing data");
    }
}
