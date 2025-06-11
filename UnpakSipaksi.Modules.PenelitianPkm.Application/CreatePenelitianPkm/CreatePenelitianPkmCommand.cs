using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.PenelitianPkm.Application.CreatePenelitianPkm
{
    public sealed record CreatePenelitianPkmCommand(
          string NIDN,
          string TahunPengajuan,
          string Judul
    ) : ICommand<Guid>;
}
