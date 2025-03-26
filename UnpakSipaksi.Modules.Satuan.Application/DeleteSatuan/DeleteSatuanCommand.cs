using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.Satuan.Application.DeleteSatuan
{
    public sealed record DeleteSatuanCommand(
        Guid uuid
    ) : ICommand;
}
