using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Modules.PenelitianHibah.Application.GetAllMemberDosen;
using UnpakSipaksi.Modules.PenelitianHibah.Application.GetMemberDosen;

namespace UnpakSipaksi.Modules.PenelitianHibah.Application.GetAllMemberDosen
{
    public sealed record GetAllMemberDosenQuery(string UuidPenelitianHibah) : IQuery<List<MemberDosenResponse>>;
}
