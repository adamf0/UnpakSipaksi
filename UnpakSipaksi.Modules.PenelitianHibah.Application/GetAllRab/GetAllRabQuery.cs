using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Modules.PenelitianHibah.Application.GetAllRab;
using UnpakSipaksi.Modules.PenelitianHibah.Application.GetRab;

namespace UnpakSipaksi.Modules.PenelitianHibah.Application.GetAllRab
{
    public sealed record GetAllRabQuery(string UuidPenelitianHibah) : IQuery<List<RabResponse>>;
}
