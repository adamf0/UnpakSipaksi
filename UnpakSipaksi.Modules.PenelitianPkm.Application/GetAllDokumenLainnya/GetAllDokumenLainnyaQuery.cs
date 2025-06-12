using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Modules.PenelitianPkm.Application.GetDokumenLainnya;

namespace UnpakSipaksi.Modules.PenelitianPkm.Application.GetAllDokumenLainnya
{
    public sealed record GetAllDokumenLainnyaQuery(string UuidPenelitianPkm) : IQuery<List<DokumenLainnyaResponse>>;
}
