using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Modules.KelompokMitra.Application.GetKelompokMitra;

namespace UnpakSipaksi.Modules.KelompokMitra.Application.GetAllKelompokMitra
{
    public sealed record GetAllKelompokMitraQuery() : IQuery<List<KelompokMitraResponse>>;
}
