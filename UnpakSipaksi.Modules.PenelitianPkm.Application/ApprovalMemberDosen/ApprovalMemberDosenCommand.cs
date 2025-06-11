using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.PenelitianPkm.Application.ApprovalMemberDosen
{
    public sealed record ApprovalMemberDosenCommand(
          string Uuid,
          string UuidPenelitianPkm,
          string NIDN,
          string Status
    ) : ICommand;
}
