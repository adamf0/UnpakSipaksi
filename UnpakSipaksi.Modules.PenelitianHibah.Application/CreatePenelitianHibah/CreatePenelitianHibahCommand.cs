using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.PenelitianHibah.Application.CreatePenelitianHibah
{
    public sealed record CreatePenelitianHibahCommand(
          string NIDN,
          string TahunPengajuan,
          string Judul
    ) : ICommand<Guid>;
}
