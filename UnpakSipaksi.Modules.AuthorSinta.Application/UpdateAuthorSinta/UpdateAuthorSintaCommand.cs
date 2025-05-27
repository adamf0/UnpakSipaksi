using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.AuthorSinta.Application.UpdateAuthorSinta
{
    public sealed record UpdateAuthorSintaCommand(
        string Uuid,
        string Nidn,
        string AuthorId,
        int Score
    ) : ICommand;
}
