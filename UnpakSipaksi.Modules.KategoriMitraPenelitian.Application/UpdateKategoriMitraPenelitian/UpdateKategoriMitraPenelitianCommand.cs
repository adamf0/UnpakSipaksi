using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.KategoriMitraPenelitian.Application.UpdateKategoriMitraPenelitian
{
    public sealed record UpdateKategoriMitraPenelitianCommand(
        string Uuid,
        string Nama
    ) : ICommand;
}
