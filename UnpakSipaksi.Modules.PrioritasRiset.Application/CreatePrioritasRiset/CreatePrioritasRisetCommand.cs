using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.PrioritasRiset.Application.CreatePrioritasRiset
{
    public sealed record CreatePrioritasRisetCommand(
        string Nama
    ) : ICommand<Guid>;
}
