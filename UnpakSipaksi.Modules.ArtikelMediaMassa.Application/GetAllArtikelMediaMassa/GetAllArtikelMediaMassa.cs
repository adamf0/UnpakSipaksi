using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Modules.ArtikelMediaMassa.Application.GetArtikelMediaMassa;

namespace UnpakSipaksi.Modules.ArtikelMediaMassa.Application.GetAllArtikelMediaMassa
{
    public sealed record GetAllArtikelMediaMassaQuery() : IQuery<List<ArtikelMediaMassaResponse>>;
}
