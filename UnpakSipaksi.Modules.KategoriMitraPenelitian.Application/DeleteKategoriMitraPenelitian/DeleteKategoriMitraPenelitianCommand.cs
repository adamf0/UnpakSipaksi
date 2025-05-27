using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.KategoriMitraPenelitian.Application.DeleteKategoriMitraPenelitian
{
    public sealed record DeleteKategoriMitraPenelitianCommand(
        string uuid
    ) : ICommand;
}
