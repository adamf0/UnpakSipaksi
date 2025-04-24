using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.PenelitianHibah.Application.UpdateMemberNonDosen
{
    public sealed record UpdateMemberNonDosenCommand(
          string Uuid,
          string UuidPenelitianHibah,
          string? NomorIdentitas,
          string? Nama,
          string? Afiliasi
    ) : ICommand;
}
