using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Modules.Administrasi.Application.GetAdministrasiPkm;

namespace UnpakSipaksi.Modules.Administrasi.Application.GetAllAdministrasiPkm
{
    public sealed record GetAllAdministrasiPkmQuery() : IQuery<List<AdministrasiPkmResponse>>;
}
