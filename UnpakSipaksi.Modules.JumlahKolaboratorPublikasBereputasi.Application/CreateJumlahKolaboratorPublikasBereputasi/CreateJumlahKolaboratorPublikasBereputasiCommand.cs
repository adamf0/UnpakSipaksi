using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.JumlahKolaboratorPublikasBereputasi.Application.CreateJumlahKolaboratorPublikasBereputasi
{
    public sealed record CreateJumlahKolaboratorPublikasBereputasiCommand(
        string Nama,
        int Skor
    ) : ICommand<Guid>;
}
