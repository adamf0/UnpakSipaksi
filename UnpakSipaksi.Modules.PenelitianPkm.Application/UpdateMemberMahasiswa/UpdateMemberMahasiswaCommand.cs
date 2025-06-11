using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.PenelitianPkm.Application.UpdateMemberMahasiswa
{
    public sealed record UpdateMemberMahasiswaCommand(
          string Uuid,
          string UuidPenelitianPkm,
          string NPM
    ) : ICommand;
}
