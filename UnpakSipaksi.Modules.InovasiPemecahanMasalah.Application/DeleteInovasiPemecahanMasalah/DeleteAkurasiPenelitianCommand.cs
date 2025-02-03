using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.InovasiPemecahanMasalah.Application.DeleteInovasiPemecahanMasalah
{
    public sealed record DeleteInovasiPemecahanMasalahCommand(
        Guid uuid
    ) : ICommand;
}
