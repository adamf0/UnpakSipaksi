using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Modules.PenelitianHibah.Application.GetDokumenPendukung;

namespace UnpakSipaksi.Modules.PenelitianHibah.Application.GetAllDokumenPendukung
{
    public sealed record GetAllDokumenPendukungQuery(string UuidPenelitianHibah) : IQuery<List<DokumenPendukungResponse>>;
}
