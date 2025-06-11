using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.PenelitianPkm.Application.UpdateMbkm
{
    public sealed record UpdateMbkmCommand(
          string Uuid,
          string UuidPenelitianPkm,
          string NPM,
          string BuktiMbkm
    ) : ICommand;
}
