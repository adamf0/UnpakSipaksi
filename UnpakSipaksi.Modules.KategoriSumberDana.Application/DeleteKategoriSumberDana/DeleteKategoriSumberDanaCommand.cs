using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.KategoriSumberDana.Application.DeleteKategoriSumberDana
{
    public sealed record DeleteKategoriSumberDanaCommand(
        Guid uuid
    ) : ICommand;
}
