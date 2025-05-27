using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.AkurasiPenelitian.Application.DeleteAkurasiPenelitian
{
    public sealed record DeleteAkurasiPenelitianCommand(
        string uuid
    ) : ICommand;
}
