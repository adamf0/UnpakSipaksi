using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Modules.PenelitianPkm.Application.GetMemberMahasiswa;

namespace UnpakSipaksi.Modules.PenelitianPkm.Application.GetAllMemberMahasiswa
{
    public sealed record GetAllMemberMahasiswaQuery(string UuidPenelitianPkm) : IQuery<List<MemberMahasiswaResponse>>;
}
