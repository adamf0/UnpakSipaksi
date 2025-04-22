using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Modules.Insentif.Domain.Insentif;

namespace UnpakSipaksi.Modules.Insentif.Application.CreateInsentifJurnal
{
    public sealed record CreateInsentifJurnalCommand(
            string Nidn,
            string JudulArtikel,
            string NamaJurnalPenerbit,
            int JumlahPenulis,
            string IndexJenisPublikasi,// Guid
            string Link,
            int JenisJurnal, //1: internal 0:external
            string Peran, //First Author, Co Author
            string TanggalTerbit,
            string? Volume,
            string? Edisi,
            string? Halaman,
            string? DOI,
            int LibatkanMahasiswa
    ) : ICommand<Guid>;
}
