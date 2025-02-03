using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Modules.KejelasanPembagianTugasTim.Application.GetKejelasanPembagianTugasTim;

namespace UnpakSipaksi.Modules.KejelasanPembagianTugasTim.Application.GetAllKejelasanPembagianTugasTim
{
    public sealed record GetAllKejelasanPembagianTugasTimQuery() : IQuery<List<KejelasanPembagianTugasTimResponse>>;
}
