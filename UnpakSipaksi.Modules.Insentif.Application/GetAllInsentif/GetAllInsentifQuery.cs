using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Modules.Insentif.Application.GetInsentif;

namespace UnpakSipaksi.Modules.Insentif.Application.GetAllInsentif
{
    public sealed record GetAllInsentifQuery() : IQuery<List<InsentifResponse>>;
}
