using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Modules.Administrasi.Application.GetAdministrasiInternal;

namespace UnpakSipaksi.Modules.Administrasi.Application.GetAdministrasiPkm
{
    public sealed record GetAdministrasiInternalQuery(Guid AdministrasiInternalUuid, Guid UuidPenelitianHibah) : IQuery<AdministrasiInternalResponse>;
}
