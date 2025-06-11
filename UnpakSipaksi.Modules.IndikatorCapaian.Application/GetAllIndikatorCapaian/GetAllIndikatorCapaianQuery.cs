using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Modules.IndikatorCapaian.Application.GetIndikatorCapaian;

namespace UnpakSipaksi.Modules.IndikatorCapaian.Application.GetAllIndikatorCapaian
{
    public sealed record GetAllIndikatorCapaianQuery() : IQuery<List<IndikatorCapaianResponse>>;
}
