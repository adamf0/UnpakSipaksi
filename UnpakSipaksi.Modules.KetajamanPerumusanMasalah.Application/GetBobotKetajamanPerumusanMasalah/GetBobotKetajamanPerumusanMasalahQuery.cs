using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.KetajamanPerumusanMasalah.Application.GetBobotKetajamanPerumusanMasalah
{
    public sealed record GetBobotKetajamanPerumusanMasalahQuery(string KategoriSkema) : IQuery<int?>;
}
