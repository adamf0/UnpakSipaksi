using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.PenelitianHibah.Application.CreateMemberNonDosen
{
    public sealed record CreateMemberNonDosenCommand(
          string UuidPenelitianHibah,
          string? NomorIdentitas,
          string? Nama,
          string? Afiliasi
    ) : ICommand<Guid>;
}
