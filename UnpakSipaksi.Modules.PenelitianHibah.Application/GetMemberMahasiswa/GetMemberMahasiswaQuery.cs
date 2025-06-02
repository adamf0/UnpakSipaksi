using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.PenelitianHibah.Application.GetMemberMahasiswa
{
    public sealed record GetMemberMahasiswaQuery(string Uuid, string UuidPenelitianHibah) : IQuery<MemberMahasiswaResponse>;
}
