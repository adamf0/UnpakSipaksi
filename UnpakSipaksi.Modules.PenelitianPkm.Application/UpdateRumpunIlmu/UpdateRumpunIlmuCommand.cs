using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.PenelitianPkm.Application.UpdateRumpunIlmu
{
    public sealed record UpdateRumpunIlmuCommand(
          string UuidPenelitianPkm,
          string? RumpunIlmu1Id,
          string? RumpunIlmu2Id,
          string? RumpunIlmu3Id
    ) : ICommand;
}
