using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.KelompokMitra.Application.GetKelompokMitra
{
    public sealed record GetKelompokMitraDefaultQuery(Guid KelompokMitraUuid) : IQuery<KelompokMitraDefaultResponse>;
}
