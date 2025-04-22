
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.JenisPublikasi.Application.GetJenisPublikasi
{
    public sealed record GetJenisPublikasiDefaultQuery(Guid JenisPublikasiUuid) : IQuery<JenisPublikasiDefaultResponse>;
}
