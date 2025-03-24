using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.Rirn.Application.GetRirn
{
    public sealed record GetRirnQuery(Guid Uuid) : IQuery<RirnResponse>;
}
