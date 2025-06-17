using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.Insentif.Domain.Insentif
{
    public static class InsentifErrors
    {
        public static Error EmptyData() =>
            Error.NotFound("Insentif.EmptyData", "data is not found");
        public static Error NotFound(Guid Id) =>
            Error.NotFound("Insentif.NotFound", $"Insentif with identifier {Id} not found");
        public static Error NotFoundIndexJenisPublikasi(string IndexJenisPublikasi) =>
            Error.NotFound("Insentif.NotFoundIndexJenisPublikasi", $"Index jenis publikasi {IndexJenisPublikasi} is not found");
        public static Error EmptyNidn() =>
            Error.NotFound("Insentif.EmptyNidn", "Nidn is empty");
        public static Error EmptyJudulArtikel() =>
            Error.NotFound("Insentif.EmptyJudulArtikel", "Judul artikel is empty");
        public static Error NotUniqueJudulArtikel() =>
            Error.NotFound("Insentif.NotUniqueJudulArtikel", "Judul artikel is not unique");
        public static Error InvalidJumlahPenulis() =>
            Error.NotFound("Insentif.InvalidJumlahPenulis", "Jumlah penulis must not be less than 1");
        public static Error EmptyNamaJurnalPenerbit() =>
            Error.NotFound("Insentif.EmptyNamaJurnalPenerbit", "Nama jurnal penerbit is empty");
        public static Error InvalidLink() =>
            Error.NotFound("Insentif.InvalidLink", "Link is invalid format");
        public static Error InvalidJenisJurnal() =>
            Error.NotFound("Insentif.InvalidJenisJurnal", "Jenis jurnal is invalid value");
        public static Error InvalidPeran() =>
            Error.NotFound("Insentif.InvalidPeran", "Peran is invalid value");
        public static Error EmptyPeran() =>
            Error.NotFound("Insentif.EmptyPeran", "Peran is empty");
        public static Error EmptyTanggalTerbit() =>
            Error.NotFound("Insentif.EmptyTanggalTerbit", "Tanggal terbit is empty");
        public static Error InvalidTanggalTerbit() =>
            Error.NotFound("Insentif.InvalidTanggalTerbit", "Tanggal terbit is invalid format date");
        public static Error InvalidJenisPublikasi() =>
            Error.NotFound("Insentif.InvalidJenisPublikasi", "Jenis publikasi is invalid format");
        public static Error EmptyLibatkanMahasiswa() =>
            Error.NotFound("Insentif.EmptyLibatkanMahasiswa", "Libatkan mahasiswa is empty");
        public static Error EmptyNamaKegiatanSeminar() =>
            Error.NotFound("Insentif.EmptyNamaKegiatanSeminar", "Nama kegiatan seminar is empty");
        public static Error InvalidDataVerifikasi() =>
            Error.NotFound("Insentif.InvalidDataVerifikasi", "Verifikasi is invalid data");
    }
}
