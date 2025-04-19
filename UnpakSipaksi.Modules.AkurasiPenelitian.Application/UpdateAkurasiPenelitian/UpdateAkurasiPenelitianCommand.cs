using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.AkurasiPenelitian.Application.UpdateAkurasiPenelitian
{
    public sealed record UpdateAkurasiPenelitianCommand(
        Guid Uuid,
        string Nama,
        int Skor
    ) : ICommand;
}
