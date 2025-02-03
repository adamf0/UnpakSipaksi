using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.ArtikelMediaMassa.Application.GetArtikelMediaMassa
{
    public sealed record GetArtikelMediaMassaQuery(Guid ArtikelMediaMassaUuid) : IQuery<ArtikelMediaMassaResponse>;
}
