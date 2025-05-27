using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.Satuan.Application.UpdateSatuan
{
    public sealed record UpdateSatuanCommand(
        string Uuid,
        string Nama
    ) : ICommand;
}
