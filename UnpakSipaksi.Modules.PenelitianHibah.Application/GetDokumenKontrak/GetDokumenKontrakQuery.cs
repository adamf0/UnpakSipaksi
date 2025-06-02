using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.PenelitianHibah.Application.GetDokumenKontrak
{
    public sealed record GetDokumenKontrakQuery(string Uuid, string UuidPenelitianHibah) : IQuery<DokumenKontrakResponse>;
}
