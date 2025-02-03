using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.KelompokMitra.Application.CreateKelompokMitra
{
    public sealed record CreateKelompokMitraCommand(
        string Nama
    ) : ICommand<Guid>;
}
