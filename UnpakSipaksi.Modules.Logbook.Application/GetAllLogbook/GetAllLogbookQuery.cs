using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Modules.Logbook.Application.GetLogbook;

namespace UnpakSipaksi.Modules.Logbook.Application.GetAllLogbook
{
    public sealed record GetAllLogbookQuery(string NIDN, string? UudPenelitianHibah, string? UudPenelitianPkm) : IQuery<List<LogbookResponse>>;
}
