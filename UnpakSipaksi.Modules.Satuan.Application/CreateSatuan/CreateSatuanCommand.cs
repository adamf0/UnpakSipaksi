using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.Satuan.Application.CreateSatuan
{
    public sealed record CreateSatuanCommand(
        string Nama
    ) : ICommand<Guid>;
}
