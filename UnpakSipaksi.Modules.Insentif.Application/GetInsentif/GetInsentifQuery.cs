using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.Insentif.Application.GetInsentif
{
    public sealed record GetInsentifQuery(Guid InsentifUuid) : IQuery<InsentifResponse>;
}
