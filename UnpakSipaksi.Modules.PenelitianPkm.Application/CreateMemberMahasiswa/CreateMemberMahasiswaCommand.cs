using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.PenelitianPkm.Application.CreateMemberMahasiswa
{
    public sealed record CreateMemberMahasiswaCommand(
          string UuidPenelitianPkm,
          string NPM
    ) : ICommand<Guid>;
}
