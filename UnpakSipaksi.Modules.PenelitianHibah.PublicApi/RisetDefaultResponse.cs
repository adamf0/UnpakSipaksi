

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.PenelitianHibah.PublicApi
{
    public sealed record RisetDefaultResponse(
        string IdPrioritasRiset,
        string UuidPrioritasRiset,
        string NamaPrioritasRiset,
        string IdFokusPenelitian,
        string UuidFokusPenelitian,
        string NamaFokusPenelitian,
        string IdTemaPenelitian,
        string UuidTemaPenelitian,
        string NamaTemaPenelitian,
        string IdTopikPenelitian,
        string UuidTopikPenelitian,
        string NamaTopikPenelitian
    );
}
