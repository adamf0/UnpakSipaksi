using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.Administrasi.Domain.AdministrasiInternal;

namespace UnpakSipaksi.Modules.Administrasi.Domain.AdministrasiPkm
{
    public sealed partial class AdministrasiPkm : Entity
    {
        private AdministrasiPkm()
        {
        }

        public string Id { get; private set; } = null;

        [Column(TypeName = "VARCHAR(36)")]
        public Guid Uuid { get; private set; }
        public int PenelitianPkmId { get; private set; }
        public string Level { get; private set; }
        public string Judul { get; private set; }
        public string? HalamanSampul { get; private set; }
        public string? HalamanPengesahan { get; private set; }
        public string? IdentitasPengusul { get; private set; }
        public string? MitraPkm { get; private set; }
        public string? LuaranTargetCapaian { get; private set; }
        public string? Rab { get; private set; }
        public string? RingkasanKataKunci { get; private set; }
        public string? AnalisisSituasi { get; private set; }
        public string? PermasalahanMitra { get; private set; }
        public string? SolusiPermasalahan { get; private set; }
        public string? MetodePelaksanaan { get; private set; }
        public string? Jadwal { get; private set; }
        public string? DaftarPustaka { get; private set; }
        public string? BiodataKetuaAnggota { get; private set; }
        public string? GambaranIptek { get; private set; }
        public string? PetaLokasiMitra { get; private set; }
        public string? SuratPernyataanKetuaPelaksana { get; private set; }
        public string? SuratKetersediaanMitra { get; private set; }
        public string? MelibatkanMahasiswa { get; private set; }
        public string? MendukungIKU { get; private set; }
        public string Keputusan { get; private set; }
        public string? Komentar { get; private set; }

        public static Result<AdministrasiPkm> Create(
            int PenelitianPkmId,
            string judul,
            string? halamanSampul,
            string? halamanPengesahan,
            string? identitasPengusul,
            string? mitraPkm,
            string? luaranTargetCapaian,
            string? rab,
            string? ringkasanKataKunci,
            string? analisisSituasi,
            string? permasalahanMitra,
            string? solusiPermasalahan,
            string? metodePelaksanaan,
            string? jadwal,
            string? daftarPustaka,
            string? biodataKetuaAnggota,
            string? gambaranIptek,
            string? petaLokasiMitra,
            string? suratPernyataanKetuaPelaksana,
            string? suratKetersediaanMitra,
            string? melibatkanMahasiswa,
            string? mendukungIKU,
            string keputusan,
            string? komentar
        )
        {
            var entity = new AdministrasiPkm
            {
                Uuid = Guid.NewGuid(),
                PenelitianPkmId = PenelitianPkmId,
                Judul = judul,
                HalamanSampul = halamanSampul,
                HalamanPengesahan = halamanPengesahan,
                IdentitasPengusul = identitasPengusul,
                MitraPkm = mitraPkm,
                LuaranTargetCapaian = luaranTargetCapaian,
                Rab = rab,
                RingkasanKataKunci = ringkasanKataKunci,
                AnalisisSituasi = analisisSituasi,
                PermasalahanMitra = permasalahanMitra,
                SolusiPermasalahan = solusiPermasalahan,
                MetodePelaksanaan = metodePelaksanaan,
                Jadwal = jadwal,
                DaftarPustaka = daftarPustaka,
                BiodataKetuaAnggota = biodataKetuaAnggota,
                GambaranIptek = gambaranIptek,
                PetaLokasiMitra = petaLokasiMitra,
                SuratPernyataanKetuaPelaksana = suratPernyataanKetuaPelaksana,
                SuratKetersediaanMitra = suratKetersediaanMitra,
                MelibatkanMahasiswa = melibatkanMahasiswa,
                MendukungIKU = mendukungIKU,
                Keputusan = keputusan,
                Komentar = komentar
            };

            var emptyFields = entity.GetContentAttributes()
                                .Where(kv =>
                                    string.IsNullOrWhiteSpace(kv.Value) ||
                                    kv.Value != Options.TidakAda.ToString() ||
                                    kv.Value != Options.AdaTapiTidakSesuai.ToString() ||
                                    kv.Value != Options.AdaSesuai.ToString())
                                .Select(kv => kv.Key)
                                .ToList();

            if (emptyFields.Any() && emptyFields.Count < 21)
            {
                return Result.Failure<AdministrasiPkm>(AdministrasiInternalErrors.InvalidArgument());
            }

            var adaSesuaiFields = entity.GetContentAttributes()
                                .Where(kv => kv.Value == Options.AdaSesuai.ToString())
                                .Select(kv => kv.Key)
                                .ToList();

            var expectationKeputusan = adaSesuaiFields.Count != 21 ? Domain.Keputusan.PerluPerbaikan.ToString() : keputusan;

            if (adaSesuaiFields.Any() && keputusan != expectationKeputusan) {
                return Result.Failure<AdministrasiPkm>(AdministrasiInternalErrors.InvalidKeputusan());
            }

            entity.Raise(new AdministrasiPkmCreatedDomainEvent(entity.Uuid));

            return entity;
        }

        public static Result<AdministrasiPkm> Update(
            Guid Uuid,
            AdministrasiPkm? prev,
            int PenelitianPkmId,
            string judul,
            string? halamanSampul,
            string? halamanPengesahan,
            string? identitasPengusul,
            string? mitraPkm,
            string? luaranTargetCapaian,
            string? rab,
            string? ringkasanKataKunci,
            string? analisisSituasi,
            string? permasalahanMitra,
            string? solusiPermasalahan,
            string? metodePelaksanaan,
            string? jadwal,
            string? daftarPustaka,
            string? biodataKetuaAnggota,
            string? gambaranIptek,
            string? petaLokasiMitra,
            string? suratPernyataanKetuaPelaksana,
            string? suratKetersediaanMitra,
            string? melibatkanMahasiswa,
            string? mendukungIKU,
            string keputusan,
            string? komentar
        )
        {
            if (prev == null)
            {
                return Result.Failure<AdministrasiPkm>(AdministrasiPkmErrors.NotFound(Uuid));
            }

            prev.PenelitianPkmId = PenelitianPkmId;
            prev.Judul = judul;
            prev.HalamanSampul = halamanSampul;
            prev.HalamanPengesahan = halamanPengesahan;
            prev.IdentitasPengusul = identitasPengusul;
            prev.MitraPkm = mitraPkm;
            prev.LuaranTargetCapaian = luaranTargetCapaian;
            prev.Rab = rab;
            prev.RingkasanKataKunci = ringkasanKataKunci;
            prev.AnalisisSituasi = analisisSituasi;
            prev.PermasalahanMitra = permasalahanMitra;
            prev.SolusiPermasalahan = solusiPermasalahan;
            prev.MetodePelaksanaan = metodePelaksanaan;
            prev.Jadwal = jadwal;
            prev.DaftarPustaka = daftarPustaka;
            prev.BiodataKetuaAnggota = biodataKetuaAnggota;
            prev.GambaranIptek = gambaranIptek;
            prev.PetaLokasiMitra = petaLokasiMitra;
            prev.SuratPernyataanKetuaPelaksana = suratPernyataanKetuaPelaksana;
            prev.SuratKetersediaanMitra = suratKetersediaanMitra;
            prev.MelibatkanMahasiswa = melibatkanMahasiswa;
            prev.MendukungIKU = mendukungIKU;
            prev.Keputusan = keputusan;
            prev.Komentar = komentar;

            var emptyFields = prev.GetContentAttributes()
                                .Where(kv =>
                                    string.IsNullOrWhiteSpace(kv.Value) ||
                                    kv.Value != Options.TidakAda.ToString() ||
                                    kv.Value != Options.AdaTapiTidakSesuai.ToString() ||
                                    kv.Value != Options.AdaSesuai.ToString())
                                .Select(kv => kv.Key)
                                .ToList();

            if (emptyFields.Any() && emptyFields.Count < 21)
            {
                return Result.Failure<AdministrasiPkm>(AdministrasiInternalErrors.InvalidArgument());
            }

            var adaSesuaiFields = prev.GetContentAttributes()
                                .Where(kv => kv.Value == Options.AdaSesuai.ToString())
                                .Select(kv => kv.Key)
                                .ToList();

            var expectationKeputusan = adaSesuaiFields.Count != 21 ? Domain.Keputusan.PerluPerbaikan.ToString() : keputusan;

            if (adaSesuaiFields.Any() && keputusan != expectationKeputusan)
            {
                return Result.Failure<AdministrasiPkm>(AdministrasiInternalErrors.InvalidKeputusan());
            }

            return prev;
        }
    }
}
