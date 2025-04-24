using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.PenelitianHibah.Application.CreateMemberMahasiswa
{
    public sealed record CreateMemberMahasiswaCommand(
          string UuidPenelitianHibah,
          string NPM
    ) : ICommand<Guid>;
}
