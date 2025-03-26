using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.RumpunIlmu3.Application.CreateRumpunIlmu3
{
    public sealed record CreateRumpunIlmu3Command(
        string Nama,
        Guid UuidRumpunIlmu2
    ) : ICommand<Guid>;
}
