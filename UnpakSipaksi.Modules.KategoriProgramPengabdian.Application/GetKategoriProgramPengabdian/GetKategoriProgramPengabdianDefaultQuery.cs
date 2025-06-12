using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.KategoriProgramPengabdian.Application.GetKategoriProgramPengabdian
{
    public sealed record GetKategoriProgramPengabdianDefaultQuery(Guid KategoriProgramPengabdianUuid) : IQuery<KategoriProgramPengabdianDefaultResponse>;
}
