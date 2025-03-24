using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.RumpunIlmu1.Application.CreateRumpunIlmu1
{
    public sealed record CreateRumpunIlmu1Command(
        string Nama
    ) : ICommand<Guid>;
}
