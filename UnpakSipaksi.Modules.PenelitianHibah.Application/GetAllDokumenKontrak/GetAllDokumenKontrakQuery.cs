using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Modules.PenelitianHibah.Application.GetDokumenKontrak;

namespace UnpakSipaksi.Modules.PenelitianHibah.Application.GetAllDokumenKontrak
{
    public sealed record GetAllDokumenKontrakQuery(string UuidPenelitianHibah) : IQuery<List<DokumenKontrakResponse>>;
}
