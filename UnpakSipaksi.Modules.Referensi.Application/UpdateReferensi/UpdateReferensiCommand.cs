using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.Referensi.Application.UpdateAkurasiPenelitian
{
    public sealed record UpdateReferensiCommand(
        Guid Uuid,
        string Nama,
        string UuidKebaruanReferensi,
        string UuidRelevansiKualitasReferensi,
        int Nilai
    ) : ICommand;
}
