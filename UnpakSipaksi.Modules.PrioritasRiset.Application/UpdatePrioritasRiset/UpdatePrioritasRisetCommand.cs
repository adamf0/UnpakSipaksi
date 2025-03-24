using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.PrioritasRiset.Application.UpdatePrioritasRiset
{
    public sealed record UpdatePrioritasRisetCommand(
        Guid Uuid,
        string Nama
    ) : ICommand;
}
