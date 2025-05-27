using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.KesesuaianPenugasan.Application.DeleteKesesuaianPenugasan
{
    public sealed record DeleteKesesuaianPenugasanCommand(
        string uuid
    ) : ICommand;
}
