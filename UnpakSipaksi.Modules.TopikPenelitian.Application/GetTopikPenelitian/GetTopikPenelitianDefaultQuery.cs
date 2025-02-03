using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.TopikPenelitian.Application.GetTopikPenelitian
{
    public sealed record GetTopikPenelitianDefaultQuery(Guid TopikPenelitianUuid) : IQuery<TopikPenelitianDefaultResponse>;
}
