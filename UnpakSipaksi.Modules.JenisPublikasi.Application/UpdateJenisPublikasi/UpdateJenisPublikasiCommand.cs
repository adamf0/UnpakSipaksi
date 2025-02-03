using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.JenisPublikasi.Application.UpdateJenisPublikasi
{
    public sealed record UpdateJenisPublikasiCommand(
        Guid Uuid,
        string Nama,
        int Sbu
    ) : ICommand;
}
