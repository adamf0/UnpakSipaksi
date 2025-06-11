using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.PenelitianPkm.Application.UpdatePenelitianPkm
{
    public sealed record UpdatePenelitianPkmCommand(
        string Uuid,
        string NIDN,
        string TahunPengajuan,
        string Judul
    ) : ICommand;
}
