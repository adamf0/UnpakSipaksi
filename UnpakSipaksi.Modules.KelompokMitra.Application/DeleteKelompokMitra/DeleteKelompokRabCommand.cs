using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.KelompokMitra.Application.DeleteKelompokMitra
{
    public sealed record DeleteKelompokMitraCommand(
        Guid uuid
    ) : ICommand;
}
