using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.RumpunIlmu2.Application.DeleteRumpunIlmu2
{
    public sealed record DeleteRumpunIlmu2Command(
        string uuid
    ) : ICommand;
}
