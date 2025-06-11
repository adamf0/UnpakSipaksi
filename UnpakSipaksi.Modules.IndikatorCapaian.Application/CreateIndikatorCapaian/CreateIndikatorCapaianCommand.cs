using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.IndikatorCapaian.Application.CreateIndikatorCapaian
{
    public sealed record CreateIndikatorCapaianCommand(
        string UuidJenisLuaran,
        string Nama,
        string? Status
    ) : ICommand<Guid>;
}
