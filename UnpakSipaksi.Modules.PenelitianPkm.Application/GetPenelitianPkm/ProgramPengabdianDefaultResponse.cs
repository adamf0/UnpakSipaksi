

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.PenelitianPkm.Application.GetPenelitianPkm
{
    public sealed record ProgramPengabdianDefaultResponse(
        string? FokusPengabdianId,
        string? UuidFokusPengabdian,
        string? NamaFokusPengabdian,
        string? RirnId,
        string? UuidRirn,
        string? NamaRirn
    );

}
