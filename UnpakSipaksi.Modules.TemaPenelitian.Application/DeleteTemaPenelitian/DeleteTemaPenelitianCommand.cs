using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.TemaPenelitian.Application.DeleteTemaPenelitian
{
    public sealed record DeleteTemaPenelitianCommand(
        string uuid
    ) : ICommand;
}
