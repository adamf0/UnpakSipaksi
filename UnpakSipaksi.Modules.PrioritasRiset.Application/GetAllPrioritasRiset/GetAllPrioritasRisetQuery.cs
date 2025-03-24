using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Modules.PrioritasRiset.Application.GetPrioritasRiset;

namespace UnpakSipaksi.Modules.PrioritasRiset.Application.GetAllPrioritasRiset
{
    public sealed record GetAllPrioritasRisetQuery() : IQuery<List<PrioritasRisetResponse>>;
}
