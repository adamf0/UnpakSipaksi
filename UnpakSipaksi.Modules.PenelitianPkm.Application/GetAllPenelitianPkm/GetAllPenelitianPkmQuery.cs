using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Modules.PenelitianPkm.Application.GetPenelitianPkm;

namespace UnpakSipaksi.Modules.PenelitianPkm.Application.GetAllPenelitianPkm
{
    public sealed record GetAllPenelitianPkmQuery() : IQuery<List<PenelitianPkmResponse>>;
}
