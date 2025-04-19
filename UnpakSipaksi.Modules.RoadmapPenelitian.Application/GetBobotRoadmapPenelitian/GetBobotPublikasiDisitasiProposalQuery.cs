using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.RoadmapPenelitian.Application.GetBobotRoadmapPenelitian
{
    public sealed record GetBobotRoadmapPenelitianQuery(string KategoriSkema) : IQuery<int?>;
}
