

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.PeningkatanKeberdayaanMitra.Application.GetPeningkatanKeberdayaanMitra
{
    public sealed record GetPeningkatanKeberdayaanMitraDefaultQuery(Guid PeningkatanKeberdayaanMitraUuid) : IQuery<PeningkatanKeberdayaanMitraDefaultResponse>;
}
