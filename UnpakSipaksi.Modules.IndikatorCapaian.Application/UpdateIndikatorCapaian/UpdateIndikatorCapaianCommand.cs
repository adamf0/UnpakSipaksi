using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.IndikatorCapaian.Application.UpdateIndikatorCapaian
{
    public sealed record UpdateIndikatorCapaianCommand(
        string Uuid,
        string UuidJenisLuaran,
        string Nama,
        string? Status
    ) : ICommand;
}
