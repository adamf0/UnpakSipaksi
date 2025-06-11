using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.PenelitianPkm.Application.DeleteLuaran
{
    public sealed record DeleteLuaranCommand(
        string Uuid,
        string UuidPenelitianPkm
    ) : ICommand;
}
