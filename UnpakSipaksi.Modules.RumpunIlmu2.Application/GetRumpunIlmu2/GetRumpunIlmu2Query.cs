using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.RumpunIlmu2.Application.GetRumpunIlmu2
{
    public sealed record GetRumpunIlmu2Query(Guid Uuid) : IQuery<RumpunIlmu2Response>;
}
