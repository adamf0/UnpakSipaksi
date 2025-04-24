using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.PenelitianHibah.Application.UpdateSkema
{
    public sealed record UpdateSkemaCommand(
          string Uuid,
          string SkemaId,
          int TKT,
          string KategoriTKT
    ) : ICommand;
}
