using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.FokusPenelitian.Application.UpdateFokusPenelitian
{
    public sealed record UpdateFokusPenelitianCommand(
        Guid Uuid,
        string Nama
    ) : ICommand;
}
