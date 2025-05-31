using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.PenelitianHibah.Application.DeleteDokumenKontrak
{
    public sealed record DeleteDokumenKontrakCommand(
        string Uuid,
        string UuidPenelitianHibah
    ) : ICommand;
}
