using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.TemaPenelitian.Application.CreateTemaPenelitian
{
    public sealed record CreateTemaPenelitianCommand(
        string Nama,
        string FokusPenelitianId
    ) : ICommand<Guid>;
}
