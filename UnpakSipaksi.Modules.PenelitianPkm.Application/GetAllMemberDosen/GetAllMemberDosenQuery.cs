using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Modules.PenelitianPkm.Application.GetMemberDosen;

namespace UnpakSipaksi.Modules.PenelitianPkm.Application.GetAllMemberDosen
{
    public sealed record GetAllMemberDosenQuery(string UuidPenelitianPkm) : IQuery<List<MemberDosenResponse>>;
}
