using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Modules.AkurasiPenelitian.Application.GetAkurasiPenelitian;

namespace UnpakSipaksi.Modules.AkurasiPenelitian.Application.GetAllAkurasiPenelitian
{
    public sealed record GetAllAkurasiPenelitianQuery() : IQuery<List<AkurasiPenelitianResponse>>;
}
