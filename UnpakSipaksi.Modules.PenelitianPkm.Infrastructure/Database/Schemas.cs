using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.PenelitianPkm.Infrastructure.Database
{
    internal static class Schemas
    {
        internal const string PenelitianPkm = "pkm";
        internal const string DosenMember = "pkm_anggota_dosen";
        internal const string MahasiswaMember = "pkm_anggota_non_dosen";
        internal const string ExternalMember = "pkm_anggota_non_dosen2";
        internal const string SubstansiUsulan = "pkm_substansi_usulan";
        internal const string Luaran = "pkm_luaran";
        internal const string RAB = "pkm_rab";
        internal const string DokumenLainnya = "pkm_dokumen_kontrak";
        internal const string DokumenMitra= "pkm_mitra";
    }
}
