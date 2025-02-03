using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Modules.TopikPenelitian.Application.GetTopikPenelitian;

namespace UnpakSipaksi.Modules.TopikPenelitian.Application.GetAllTopikPenelitian
{
    public sealed record GetAllTopikPenelitianQuery() : IQuery<List<TopikPenelitianResponse>>;
}
