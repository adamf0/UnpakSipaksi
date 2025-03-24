using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.RumpunIlmu1.Application.GetRumpunIlmu1
{
    public sealed record GetRumpunIlmu1Query(Guid Uuid) : IQuery<RumpunIlmu1Response>;
}
