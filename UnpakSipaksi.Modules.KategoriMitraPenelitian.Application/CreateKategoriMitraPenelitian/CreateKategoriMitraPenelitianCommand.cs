using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.KategoriMitraPenelitian.Application.CreateKategoriMitraPenelitian
{
    public sealed record CreateKategoriMitraPenelitianCommand(
        string Nama
    ) : ICommand<Guid>;
}
