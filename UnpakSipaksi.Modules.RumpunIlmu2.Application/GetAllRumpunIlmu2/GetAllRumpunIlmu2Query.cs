using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Modules.RumpunIlmu2.Application.GetRumpunIlmu2;

namespace UnpakSipaksi.Modules.RumpunIlmu2.Application.GetAllRumpunIlmu2
{
    public sealed record GetAllRumpunIlmu2Query() : IQuery<List<RumpunIlmu2Response>>;
}
