using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.RumpunIlmu3.Application.DeleteRumpunIlmu3
{
    public sealed record DeleteRumpunIlmu3Command(
        string uuid
    ) : ICommand;
}
