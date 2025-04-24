using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.PenelitianHibah.PublicApi
{
    public sealed record PenelitianHibahResponse(
        string Id,
        string Uuid,
        string NIDN,
        string Judul,
        string TahunPengajuan,
        SkemaDefaultResponse? Skema,
        RisetDefaultResponse? Riset,
        RumpunIlmuDefaultResponse? RumpunIlmu,
        int? LamaKegiatan,
        string Status,
        string? Type
    );
}
