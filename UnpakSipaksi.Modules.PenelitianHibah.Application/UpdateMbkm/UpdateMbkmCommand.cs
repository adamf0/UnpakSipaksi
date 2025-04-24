using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.PenelitianHibah.Application.UpdateMbkm
{
    public sealed record UpdateMbkmCommand(
          string Uuid,
          string UuidPenelitianHibah,
          string NPM,
          string BuktiMbkm
    ) : ICommand;
}
