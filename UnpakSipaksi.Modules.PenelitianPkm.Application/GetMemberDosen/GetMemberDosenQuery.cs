using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.PenelitianPkm.Application.GetMemberDosen
{
    public sealed record GetMemberDosenQuery(string Uuid, string UuidPenelitianPkm) : IQuery<MemberDosenResponse>;
}
