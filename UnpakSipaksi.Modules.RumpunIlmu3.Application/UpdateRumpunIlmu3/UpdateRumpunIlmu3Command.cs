using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.RumpunIlmu3.Application.UpdateRumpunIlmu3
{
    public sealed record UpdateRumpunIlmu3Command(
        Guid Uuid,
        string Nama,
        Guid UuidRumpunIlmu2
    ) : ICommand;
}
