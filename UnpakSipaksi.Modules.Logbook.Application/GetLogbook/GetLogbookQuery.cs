using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.Logbook.Application.GetLogbook
{
    public sealed record GetLogbookQuery(string Uuid, string NIDN, string? UudPenelitianHibah, string? UudPenelitianPkm) : IQuery<LogbookResponse>;
}
