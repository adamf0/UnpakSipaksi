using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.Insentif.Application.DeleteInsentif
{
    public sealed record DeleteInsentifCommand(
        Guid uuid
    ) : ICommand;
}
