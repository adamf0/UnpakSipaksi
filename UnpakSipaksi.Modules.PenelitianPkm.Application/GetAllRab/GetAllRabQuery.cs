using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Modules.PenelitianPkm.Application.GetRab;

namespace UnpakSipaksi.Modules.PenelitianPkm.Application.GetAllRab
{
    public sealed record GetAllRabQuery(string UuidPenelitianPkm) : IQuery<List<RabResponse>>;
}
