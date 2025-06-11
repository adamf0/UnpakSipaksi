using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.PenelitianPkm.Application.UpdateMemberDosen
{
    public sealed record UpdateMemberDosenCommand(
          string Uuid,
          string UuidPenelitianPkm,
          string NIDN
    ) : ICommand;
}
