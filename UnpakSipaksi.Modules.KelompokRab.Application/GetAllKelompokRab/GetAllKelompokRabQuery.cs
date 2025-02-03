using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Modules.KelompokRab.Application.GetKelompokRab;

namespace UnpakSipaksi.Modules.KelompokRab.Application.GetAllKelompokRab
{
    public sealed record GetAllKelompokRabQuery() : IQuery<List<KelompokRabResponse>>;
}
