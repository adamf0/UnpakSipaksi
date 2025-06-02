using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Modules.PenelitianHibah.Application.GetAllMemberMahasiswa;
using UnpakSipaksi.Modules.PenelitianHibah.Application.GetMemberMahasiswa;

namespace UnpakSipaksi.Modules.PenelitianHibah.Application.GetAllMemberMahasiswa
{
    public sealed record GetAllMemberMahasiswaQuery(string UuidPenelitianHibah) : IQuery<List<MemberMahasiswaResponse>>;
}
