using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.FokusPenelitian.Application.GetFokusPenelitian
{
    public sealed record GetFokusPenelitianDefaultQuery(Guid FokusPenelitianUuid) : IQuery<FokusPenelitianDefaultResponse>;
}
