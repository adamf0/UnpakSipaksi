using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.PenelitianHibah.Application.DeletePenelitianHibah
{
    public sealed record DeletePenelitianHibahCommand(
        Guid Uuid,
        string Nidn
    ) : ICommand;
}
