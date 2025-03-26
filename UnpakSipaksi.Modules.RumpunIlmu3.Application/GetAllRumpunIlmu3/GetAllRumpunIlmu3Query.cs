using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Modules.RumpunIlmu3.Application.GetRumpunIlmu3;

namespace UnpakSipaksi.Modules.RumpunIlmu3.Application.GetAllRumpunIlmu3
{
    public sealed record GetAllRumpunIlmu3Query() : IQuery<List<RumpunIlmu3Response>>;
}
