using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.KesesuaianPenugasan.Application.CreateKesesuaianPenugasan
{
    public sealed record CreateKesesuaianPenugasanCommand(
        string Nama,
        int Nilai
    ) : ICommand<Guid>;
}
