using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.KelompokRab.Application.UpdateKelompokRab
{
    public sealed record UpdateKelompokRabCommand(
        string Uuid,
        string Nama
    ) : ICommand;
}
