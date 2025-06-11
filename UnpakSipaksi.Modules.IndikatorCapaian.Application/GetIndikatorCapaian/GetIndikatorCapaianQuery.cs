using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.IndikatorCapaian.Application.GetIndikatorCapaian
{
    public sealed record GetIndikatorCapaianQuery(Guid IndikatorCapaianUuid) : IQuery<IndikatorCapaianResponse>;
}
