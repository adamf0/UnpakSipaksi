using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.PenelitianHibah.Application.UpdatePenelitianHibah
{
    public sealed record UpdatePenelitianHibahCommand(
        string Uuid,
        string NIDN,
        string TahunPengajuan,
        string Judul
    ) : ICommand;
}
