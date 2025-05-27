using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.Rirn.Application.UpdateRirn
{
    public sealed record UpdateRirnCommand(
        string Uuid,
        string Nama
    ) : ICommand;
}
