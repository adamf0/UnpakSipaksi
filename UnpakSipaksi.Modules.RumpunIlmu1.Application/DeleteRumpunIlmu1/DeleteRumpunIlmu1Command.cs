using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.RumpunIlmu1.Application.DeleteRumpunIlmu1
{
    public sealed record DeleteRumpunIlmu1Command(
        Guid uuid
    ) : ICommand;
}
