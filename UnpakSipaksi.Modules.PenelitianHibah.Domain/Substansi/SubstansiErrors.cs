using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.PenelitianHibah.Domain.Substansi
{
    public static class SubstansiErrors
    {
        public static Error EmptyData() =>
            Error.NotFound("Substansi.EmptyData", "data is not found");
        public static Error EmptyFile() =>
            Error.NotFound("Substansi.EmptyFile", "file can't be empty");
        public static Error NotFound(Guid Id) =>
            Error.NotFound("Substansi.NotFound", $"Substansi file with identifier {Id} not found");
        public static Error NotFoundHibah(Guid Id) =>
            Error.NotFound("Substansi.NotFoundHibah", $"Penelitian hibah with identifier {Id} not found");

    }
}
