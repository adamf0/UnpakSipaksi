using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Modules.JumlahKolaboratorPublikasBereputasi.Application.GetJumlahKolaboratorPublikasBereputasi;

namespace UnpakSipaksi.Modules.JumlahKolaboratorPublikasBereputasi.Application.GetAllJumlahKolaboratorPublikasBereputasi
{
    public sealed record GetAllJumlahKolaboratorPublikasBereputasiQuery() : IQuery<List<JumlahKolaboratorPublikasBereputasiResponse>>;
}
