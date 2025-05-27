using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.JumlahKolaboratorPublikasBereputasi.Application.UpdateJumlahKolaboratorPublikasBereputasi
{
    public sealed record UpdateJumlahKolaboratorPublikasBereputasiCommand(
        string Uuid,
        string Nama,
        int Skor
    ) : ICommand;
}
