using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.KategoriTkt.Application.UpdateKategoriTkt
{
    public sealed record UpdateKategoriTktCommand(
        Guid Uuid,
        string Nama
    ) : ICommand;
}
