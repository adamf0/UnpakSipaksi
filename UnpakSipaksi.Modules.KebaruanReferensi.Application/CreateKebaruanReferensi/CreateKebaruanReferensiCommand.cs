using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.KebaruanReferensi.Application.CreateKebaruanReferensi
{
    public sealed record CreateKebaruanReferensiCommand(
        string Nama,
        int Skor
    ) : ICommand<Guid>;
}
