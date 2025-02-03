using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.ArtikelMediaMassa.Application.CreateArtikelMediaMassa
{
    public sealed record CreateArtikelMediaMassaCommand(
        string Nama,
        int Nilai
    ) : ICommand<Guid>;
}
