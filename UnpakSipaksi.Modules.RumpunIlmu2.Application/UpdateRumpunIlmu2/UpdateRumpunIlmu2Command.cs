using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.RumpunIlmu2.Application.UpdateRumpunIlmu2
{
    public sealed record UpdateRumpunIlmu2Command(
        string Uuid,
        string Nama,
        string UuidRumpunIlmu1
    ) : ICommand;
}
