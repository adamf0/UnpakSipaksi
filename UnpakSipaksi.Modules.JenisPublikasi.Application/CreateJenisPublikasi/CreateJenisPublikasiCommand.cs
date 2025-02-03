using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.JenisPublikasi.Application.CreateJenisPublikasi
{
    public sealed record CreateJenisPublikasiCommand(
        string Nama,
        int Sbu
    ) : ICommand<Guid>;
}
