using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.TemaPenelitian.Application.UpdateTemaPenelitian
{
    public sealed record UpdateTemaPenelitianCommand(
        string Uuid,
        string Nama,
        string FokusPenelitianId
    ) : ICommand;
}
