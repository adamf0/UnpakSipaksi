using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.AkurasiPenelitian.Application.CreateAkurasiPenelitian
{
    public sealed record CreateAkurasiPenelitianCommand(
        string Nama,
        int Skor
    ) : ICommand<Guid>;
}
