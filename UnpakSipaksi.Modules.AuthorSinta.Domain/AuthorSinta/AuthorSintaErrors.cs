using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.AuthorSinta.Domain.AuthorSinta
{
    public static class AuthorSintaErrors
    {
        public static Error EmptyData() =>
            Error.NotFound("AuthorSinta.EmptyData", "data is not found");
        public static Error InvalidAuthorId() =>
            Error.NotFound("AuthorSinta.InvalidAuthorId", "Author identity is invalid format");
        public static Error InvalidSkor() =>
            Error.NotFound("AuthorSinta.InvalidSkor", "Skor is invalid format");

        public static Error NotFound(Guid Id) =>
            Error.NotFound("AuthorSinta.NotFound", $"Author sinta with identifier {Id} not found");

    }
}
