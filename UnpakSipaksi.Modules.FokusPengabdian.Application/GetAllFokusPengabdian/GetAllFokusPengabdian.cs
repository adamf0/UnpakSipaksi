using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Modules.FokusPengabdian.Application.GetFokusPengabdian;

namespace UnpakSipaksi.Modules.FokusPengabdian.Application.GetAllFokusPengabdian
{
    public sealed record GetAllFokusPengabdianQuery() : IQuery<List<FokusPengabdianResponse>>;
}
