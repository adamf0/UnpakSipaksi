using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Modules.PenelitianHibah.Application.GetPenelitianHibah;

namespace UnpakSipaksi.Modules.PenelitianHibah.Application.GetAllPenelitianHibah
{
    public sealed record GetAllPenelitianHibahQuery() : IQuery<List<PenelitianHibahResponse>>;
}
