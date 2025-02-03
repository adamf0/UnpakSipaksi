using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Modules.JenisPublikasi.Application.GetJenisPublikasi;

namespace UnpakSipaksi.Modules.JenisPublikasi.Application.GetAllJenisPublikasi
{
    public sealed record GetAllJenisPublikasiQuery() : IQuery<List<JenisPublikasiResponse>>;
}
