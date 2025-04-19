using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.KredibilitasMitraDukungan.Application.GetBobotKredibilitasMitraDukungan
{
    public sealed record GetBobotKredibilitasMitraDukunganQuery(string KategoriSkema) : IQuery<int?>;
}
