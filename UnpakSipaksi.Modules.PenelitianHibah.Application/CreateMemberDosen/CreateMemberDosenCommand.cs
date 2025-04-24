using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.PenelitianHibah.Application.CreateMemberDosen
{
    public sealed record CreateMemberDosenCommand(
          string UuidPenelitianHibah,
          string NIDN
    ) : ICommand<Guid>;
}
