using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.RumpunIlmu2.Application.CreateRumpunIlmu2
{
    public sealed record CreateRumpunIlmu2Command(
        string Nama,
        Guid UuidRumpunIlmu1
    ) : ICommand<Guid>;
}
