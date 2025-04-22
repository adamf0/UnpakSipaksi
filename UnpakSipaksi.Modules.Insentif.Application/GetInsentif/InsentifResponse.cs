using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.Insentif.Application.GetInsentif
{
    public sealed record InsentifResponse
    {
        public string Uuid { get; set; }
        public string Nidn { get; set; }
        public string JudulArtikel { get; set; }
        public string NamaJurnalPenerbit { get; set; }
        public int JumlahPenulis { get; set; }
        public string IndexJenisPublikasi { get; set; } //Guid
        public string Link { get; set; }
        public string JenisJurnal { get; set; }
        public string Peran { get; set; }
        public string TanggalTerbit { get; set; }
        public string? Volume { get; set; }
        public string? Edisi { get; set; }
        public string? Halaman { get; set; }
        public string? DOI { get; set; }
        public string? NamaKegiatanSeminar { get; set; }
        public string LibatkanMahasiswa { get; set; }
        public decimal Insentif { get; set; }
        public string Porsi { get; set; }
    }
}
