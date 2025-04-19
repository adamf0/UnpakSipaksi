using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.Referensi.Application.GetReferensi
{
    public sealed record GetReferensiDefaultQuery(Guid ReferensiUuid) : IQuery<ReferensiDefaultResponse>;
}
