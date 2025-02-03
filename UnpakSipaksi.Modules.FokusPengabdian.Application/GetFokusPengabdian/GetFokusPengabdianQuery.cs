using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.FokusPengabdian.Application.GetFokusPengabdian
{
    public sealed record GetFokusPengabdianQuery(Guid FokusPengabdianUuid) : IQuery<FokusPengabdianResponse>;
}
