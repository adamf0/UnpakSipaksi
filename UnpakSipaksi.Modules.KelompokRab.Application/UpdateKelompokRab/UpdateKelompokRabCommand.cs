using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.KelompokRab.Application.UpdateKelompokRab
{
    public sealed record UpdateKelompokRabCommand(
        Guid Uuid,
        string Nama
    ) : ICommand;
}
