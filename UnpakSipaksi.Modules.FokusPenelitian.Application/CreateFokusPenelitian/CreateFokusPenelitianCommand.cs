using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.FokusPenelitian.Application.CreateFokusPenelitian
{
    public sealed record CreateFokusPenelitianCommand(
        string Nama
    ) : ICommand<Guid>;
}
