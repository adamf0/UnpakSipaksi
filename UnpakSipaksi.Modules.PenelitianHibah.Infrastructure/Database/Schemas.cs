using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.PenelitianHibah.Infrastructure.Database
{
    internal static class Schemas
    {
        internal const string PenelitianHibah = "penelitian_internal";
        internal const string DosenMember = "penelitian_internal_anggota_dosen";
        internal const string MahasiswaMember = "penelitian_internal_anggota_non_dosen";
        internal const string ExternalMember = "penelitian_internal_anggota_non_dosen2";
        internal const string SubstansiUsulan = "penelitian_internal_substansi_usulan";
        internal const string Luaran = "penelitian_internal_luaran";
        internal const string RAB = "penelitian_internal_rab";
        internal const string DokumenKontrak = "penelitian_internal_dokumen_kontrak";
        internal const string DokumenPendukung = "penelitian_internal_dokumen_pendukung";
    }
}
