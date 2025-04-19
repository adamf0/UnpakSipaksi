using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.ModelFeasibilityStudys.Application.GetBobotModelFeasibilityStudys
{
    public sealed record GetBobotModelFeasibilityStudysQuery(string KategoriSkema) : IQuery<int?>;
}
