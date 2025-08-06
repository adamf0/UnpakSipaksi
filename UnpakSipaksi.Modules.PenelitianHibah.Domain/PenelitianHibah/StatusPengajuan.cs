using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.PenelitianHibah.Domain.PenelitianHibah
{
    public enum StatusPengajuan
    {
        [Display(Name = "Draf")]
        Draf,

        [Display(Name = "Tolak")]
        Tolak,

        [Display(Name = "Tidak di danai")]
        TidakDiDanai,

        [Display(Name = "Tolak Anggota")]
        TolakAnggota,

        [Display(Name = "Sudah Di Nilai")]
        SudahDiNilai,

        [Display(Name = "Di danai")]
        DiDanai,

        [Display(Name = "Perbaikan")]
        Perbaikan,

        [Display(Name = "Menunggu Verifikasi Anggota")]
        MenungguAnggota,

        [Display(Name = "Menunggu Review Fakultas")]
        MenungguReviewFakultas,

        [Display(Name = "Menunggu Review LPPM")]
        MenungguReviewLppm,

        [Display(Name = "Menunggu Pilih Reviewer")]
        MenungguPilihReviewer,

        [Display(Name = "Menunggu Reviewer")]
        MenungguReviewer
    }

}
