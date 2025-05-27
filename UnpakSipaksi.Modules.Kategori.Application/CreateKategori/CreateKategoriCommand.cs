using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.Kategori.Application.CreateKategori
{
    public sealed record CreateKategoriCommand(
        string Nama
    ) : ICommand<Guid>;
}
