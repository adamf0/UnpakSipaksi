using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Modules.AuthorSinta.Application.GetAuthorSinta;

namespace UnpakSipaksi.Modules.AuthorSinta.Application.GetAllAuthorSinta
{
    public sealed record GetAllAuthorSintaQuery() : IQuery<List<AuthorSintaResponse>>;
}
