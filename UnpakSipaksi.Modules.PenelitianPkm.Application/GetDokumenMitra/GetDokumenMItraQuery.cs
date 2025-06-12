using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.PenelitianPkm.Application.GetDokumenMitra
{
    public sealed record GetDokumenMitraQuery(string Uuid, string UuidPenelitianPkm) : IQuery<DokumenMitraResponse>;
}
