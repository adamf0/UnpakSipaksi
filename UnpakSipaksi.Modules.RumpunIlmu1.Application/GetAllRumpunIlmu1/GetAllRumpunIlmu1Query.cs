using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Modules.RumpunIlmu1.Application.GetRumpunIlmu1;

namespace UnpakSipaksi.Modules.RumpunIlmu1.Application.GetAllRumpunIlmu1
{
    public sealed record GetAllRumpunIlmu1Query() : IQuery<List<RumpunIlmu1Response>>;
}
