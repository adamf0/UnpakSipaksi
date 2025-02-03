using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.KejelasanPembagianTugasTim.Application.GetKejelasanPembagianTugasTim
{
    public sealed record GetKejelasanPembagianTugasTimQuery(Guid KejelasanPembagianTugasTimUuid) : IQuery<KejelasanPembagianTugasTimResponse>;
}
