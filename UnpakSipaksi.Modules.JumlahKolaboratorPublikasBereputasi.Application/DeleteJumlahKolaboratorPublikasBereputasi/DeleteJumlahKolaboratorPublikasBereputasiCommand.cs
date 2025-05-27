using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.JumlahKolaboratorPublikasBereputasi.Application.DeleteJumlahKolaboratorPublikasBereputasi
{
    public sealed record DeleteJumlahKolaboratorPublikasBereputasiCommand(
        string uuid
    ) : ICommand;
}
