using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.PenelitianHibah.Application.DeleteMemberNonDosen
{
    public sealed record DeleteMemberNonDosenCommand(
        string Uuid
    ) : ICommand;
}
