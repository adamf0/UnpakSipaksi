using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Modules.Administrasi.Application.GetAdministrasiInternal;

namespace UnpakSipaksi.Modules.Administrasi.Application.GetAllAdministrasiInternal
{
    public sealed record GetAllAdministrasiInternalQuery(Guid UuidPenelitianHibah) : IQuery<List<AdministrasiInternalResponse>>;
}
