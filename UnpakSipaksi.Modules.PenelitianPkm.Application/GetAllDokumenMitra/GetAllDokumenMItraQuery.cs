using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Modules.PenelitianPkm.Application.GetDokumenMitra;

namespace UnpakSipaksi.Modules.PenelitianPkm.Application.GetAllDokumenMitra
{
    public sealed record GetAllDokumenMitraQuery(string UuidPenelitianPkm) : IQuery<List<DokumenMitraResponse>>;
}
