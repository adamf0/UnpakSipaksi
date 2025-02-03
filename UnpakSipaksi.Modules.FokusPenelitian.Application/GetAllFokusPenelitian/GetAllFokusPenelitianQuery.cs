using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Modules.FokusPenelitian.Application.GetFokusPenelitian;

namespace UnpakSipaksi.Modules.FokusPenelitian.Application.GetAllFokusPenelitian
{
    public sealed record GetAllFokusPenelitianQuery() : IQuery<List<FokusPenelitianResponse>>;
}
