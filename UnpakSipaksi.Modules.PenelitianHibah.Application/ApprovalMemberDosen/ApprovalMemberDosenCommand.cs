using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.PenelitianHibah.Application.ApprovalMemberDosen
{
    public sealed record ApprovalMemberDosenCommand(
          string Uuid,
          string UuidPenelitianHibah,
          string NIDN,
          string Status
    ) : ICommand;
}
