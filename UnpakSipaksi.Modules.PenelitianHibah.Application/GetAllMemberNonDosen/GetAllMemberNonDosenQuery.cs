using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Modules.PenelitianHibah.Application.GetAllMemberNonDosen;
using UnpakSipaksi.Modules.PenelitianHibah.Application.GetMemberNonDosen;

namespace UnpakSipaksi.Modules.PenelitianHibah.Application.GetAllMemberNonDosen
{
    public sealed record GetAllMemberNonDosenQuery(string UuidPenelitianHibah) : IQuery<List<MemberNonDosenResponse>>;
}
