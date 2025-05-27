using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.RumpunIlmu1.Application.UpdateRumpunIlmu1
{
    public sealed record UpdateRumpunIlmu1Command(
        string Uuid,
        string Nama
    ) : ICommand;
}
