using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Modules.Rirn.Application.GetRirn;

namespace UnpakSipaksi.Modules.Rirn.Application.GetAllRirn
{
    public sealed record GetAllRirnQuery() : IQuery<List<RirnResponse>>;
}
