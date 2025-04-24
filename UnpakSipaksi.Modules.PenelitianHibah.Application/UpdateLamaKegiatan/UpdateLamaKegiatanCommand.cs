using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.PenelitianHibah.Application.UpdateLamaKegiatan
{
    public sealed record UpdateMemberDosenCommand(
          string Uuid,
          int LamaKegiatan
    ) : ICommand;
}
