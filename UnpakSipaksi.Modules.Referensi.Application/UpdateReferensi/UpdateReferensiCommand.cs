using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.Referensi.Application.UpdateReferensi
{
    public sealed record UpdateReferensiCommand(
        string Uuid,
        string Nama,
        string UuidKebaruanReferensi,
        string UuidRelevansiKualitasReferensi,
        int Nilai
    ) : ICommand;
}
