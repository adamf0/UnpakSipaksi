using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Modules.Insentif.Domain.Insentif;

namespace UnpakSipaksi.Modules.Insentif.Application.CreateReviewInsentif
{
    public sealed record CreateReviewInsentifCommand(
            string Uuid,
            string BuktiPublikasi,
            string StatusJurnal,
            string PeranPenulis,
            int JumlahPenulis,
            string JenisJurnal,
            string LibatkanMahasiswa,
            string StatusPengajuan,
            string? Catatan,
            string Type
    ) : ICommand<Guid>;
}
