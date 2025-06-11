using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.PenelitianPkm.Application.CreateMemberDosen
{
    public sealed record CreateMemberDosenCommand(
          string UuidPenelitianPkm,
          string NIDN
    ) : ICommand<Guid>;
}
