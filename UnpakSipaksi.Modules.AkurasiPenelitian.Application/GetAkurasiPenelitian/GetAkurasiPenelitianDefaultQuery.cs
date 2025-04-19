using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.AkurasiPenelitian.Application.GetAkurasiPenelitian
{
    public sealed record GetAkurasiPenelitianDefaultQuery(Guid AkurasiPenelitianUuid) : IQuery<AkurasiPenelitianDefaultResponse>;
}
