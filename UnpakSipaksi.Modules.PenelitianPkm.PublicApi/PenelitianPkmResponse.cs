using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.PenelitianPkm.PublicApi
{
    public sealed record PenelitianPkmResponse(
        string Id,
        string Uuid,
        string NIDN,
        string Judul,
        string TahunPengajuan,
        object? KategoriProgramPengabdian,
        object? ProgramPengabdian,
        RumpunIlmuDefaultResponse? RumpunIlmu,
        string Status,
        string? Type
    );
}
