using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.Administrasi.Application.GetAdministrasiPkm
{
    public sealed record GetAdministrasiPkmQuery(Guid AdministrasiPkmUuid) : IQuery<AdministrasiPkmResponse>;
}
