using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.Kategori.Application.DeleteKategori
{
    public sealed record DeleteKategoriCommand(
        string uuid
    ) : ICommand;
}
