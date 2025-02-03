using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.FokusPengabdian.Application.CreateFokusPengabdian
{
    public sealed record CreateFokusPengabdianCommand(
        string Nama
    ) : ICommand<Guid>;
}
