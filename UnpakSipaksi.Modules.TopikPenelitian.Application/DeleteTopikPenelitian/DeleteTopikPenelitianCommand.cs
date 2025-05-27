using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.TopikPenelitian.Application.DeleteTopikPenelitian
{
    public sealed record DeleteTopikPenelitianCommand(
        string uuid
    ) : ICommand;
}
