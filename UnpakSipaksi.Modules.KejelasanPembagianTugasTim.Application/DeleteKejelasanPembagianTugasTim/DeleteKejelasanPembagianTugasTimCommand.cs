using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.KejelasanPembagianTugasTim.Application.DeleteKejelasanPembagianTugasTim
{
    public sealed record DeleteKejelasanPembagianTugasTimCommand(
        Guid uuid
    ) : ICommand;
}
