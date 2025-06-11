using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Modules.PenelitianPkm.Application.GetMemberNonDosen;

namespace UnpakSipaksi.Modules.PenelitianPkm.Application.GetAllMemberNonDosen
{
    public sealed record GetAllMemberNonDosenQuery(string UuidPenelitianPkm) : IQuery<List<MemberNonDosenResponse>>;
}
