using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.TopikPenelitian.Application.UpdateTopikPenelitian
{
    public sealed record UpdateTopikPenelitianCommand(
        Guid Uuid,
        string Nama,
        Guid TemaPenelitianId
    ) : ICommand;
}
