using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.Logbook.Application.DeleteLogbook
{
    public sealed record DeleteLogbookCommand(
        string Uuid,
        string? UuidPenelitianHibah,
        string? UuidPenelitianPkm
    ) : ICommand;
}
