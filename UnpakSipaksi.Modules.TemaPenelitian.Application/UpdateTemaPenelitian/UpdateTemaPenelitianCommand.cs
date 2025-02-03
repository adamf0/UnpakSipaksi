using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.TemaPenelitian.Application.UpdateTemaPenelitian
{
    public sealed record UpdateTemaPenelitianCommand(
        Guid Uuid,
        string Nama,
        Guid FokusPenelitianId
    ) : ICommand;
}
