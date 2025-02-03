using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.AuthorSinta.Application.DeleteAuthorSinta
{
    public sealed record DeleteAuthorSintaCommand(
        Guid uuid
    ) : ICommand;
}
