using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.PenelitianPkm.Application.CreateMemberNonDosen
{
    public sealed record CreateMemberNonDosenCommand(
          string UuidPenelitianPkm,
          string? NomorIdentitas,
          string? Nama,
          string? Afiliasi
    ) : ICommand<Guid>;
}
