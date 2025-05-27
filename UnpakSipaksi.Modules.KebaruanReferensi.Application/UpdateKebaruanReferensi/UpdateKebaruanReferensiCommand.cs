using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.KebaruanReferensi.Application.UpdateKebaruanReferensi
{
    public sealed record UpdateKebaruanReferensiCommand(
        string Uuid,
        string Nama,
        int Skor
    ) : ICommand;
}
