using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.Administrasi.Domain.AdministrasiPkm;

namespace UnpakSipaksi.Modules.Administrasi.Domain.AdministrasiInternal
{
    public sealed partial class AdministrasiInternal : Entity
    {
        private AdministrasiInternal()
        {
        }

        public string? Id { get; private set; } = null;

        [Column(TypeName = "VARCHAR(36)")]
        public Guid Uuid { get; private set; }
        public int PenelitianInternalId { get; private set; }
        public string Level { get; private set; }
        public string? Judul { get; private set; }
        public string? HalamanSampul { get; private set; }
        public string? HalamanPengesahan { get; private set; }
        public string? IdentitasPengusul { get; private set; }
        public string? IdentitasMahasiswa { get; private set; }
        public string? MitraKerjasama { get; private set; }
        public string? LuaranTargetCapaian { get; private set; }
        public string? Rab { get; private set; }
        public string? LatarBelakangRumusanMasalah { get; private set; }
        public string PendekatanPemecahanMasalah { get; private set; }
        public string? Sota { get; private set; }
        public string? PenjelasanCapaianRisetKepakaran { get; private set; }
        public string? PetaJalan { get; private set; }
        public string? TahapanPenelitian { get; private set; }
        public string? IndikatorCapaianPadaRab { get; private set; }
        public string? Jadwal { get; private set; }
        public string? DaftarPustaka { get; private set; }
        public string? BiodataKetuaAnggota { get; private set; }
        public string? PaktaIntegritas { get; private set; }
        public string? SuratKetersediaanMitra { get; private set; }
        public string Keputusan { get; private set; }
        public string? Komentar { get; private set; }

        public static Result<AdministrasiInternal> Create(
            int PenelitianInternalId,
            string level,
            string? judul,
            string? halamanSampul,
            string? halamanPengesahan,
            string? identitasPengusul,
            string? identitasMahasiswa,
            string? mitraKerjasama,
            string? luaranTargetCapaian,
            string? rab,
            string? latarBelakangRumusanMasalah,
            string? pendekatanPemecahanMasalah,
            string? sota,
            string? penjelasanCapaianRisetKepakaran,
            string? petaJalan,
            string? tahapanPenelitian,
            string? indikatorCapaianPadaRab,
            string? jadwal,
            string? daftarPustaka,
            string? biodataKetuaAnggota,
            string? paktaIntegritas,
            string? suratKetersediaanMitra,
            string keputusan,
            string? komentar
        )
        {
            var entity = new AdministrasiInternal
            {
                Uuid = Guid.NewGuid(),
                PenelitianInternalId = PenelitianInternalId,
                Level = level,
                Judul = judul,
                HalamanSampul = halamanSampul,
                HalamanPengesahan = halamanPengesahan,
                IdentitasPengusul = identitasPengusul,
                IdentitasMahasiswa = identitasMahasiswa,
                MitraKerjasama = mitraKerjasama,
                LuaranTargetCapaian = luaranTargetCapaian,
                Rab = rab,
                LatarBelakangRumusanMasalah = latarBelakangRumusanMasalah,
                PendekatanPemecahanMasalah = pendekatanPemecahanMasalah,
                Sota = sota,
                PenjelasanCapaianRisetKepakaran = penjelasanCapaianRisetKepakaran,
                PetaJalan = petaJalan,
                TahapanPenelitian = tahapanPenelitian,
                IndikatorCapaianPadaRab = indikatorCapaianPadaRab,
                Jadwal = jadwal,
                DaftarPustaka = daftarPustaka,
                BiodataKetuaAnggota = biodataKetuaAnggota,
                PaktaIntegritas = paktaIntegritas,
                SuratKetersediaanMitra = suratKetersediaanMitra,
                Keputusan = keputusan,
                Komentar = komentar
            };

            var emptyFields = entity.GetContentAttributes()
                                .Where(kv => string.IsNullOrWhiteSpace(kv.Value))
                                .Select(kv => kv.Key)
                                .ToList();

            var adaSesuaiFields = entity.GetContentAttributes()
                                .Where(kv => kv.Value == Options.AdaSesuai.ToString())
                                .Select(kv => kv.Key)
                                .ToList();

            var expectationKeputusan = adaSesuaiFields.Count != 21 ? Domain.Keputusan.PerluPerbaikan.ToString() : keputusan;

            if (emptyFields.Any() && emptyFields.Count < 21)
            {
                return Result.Failure<AdministrasiInternal>(AdministrasiInternalErrors.InvalidArgument());
            }
            if (adaSesuaiFields.Any() && keputusan != expectationKeputusan)
            {
                return Result.Failure<AdministrasiInternal>(AdministrasiInternalErrors.InvalidKeputusan());
            }

            entity.Raise(new AdministrasiInternalCreatedDomainEvent(entity.Uuid));

            return entity;
        }

        public static Result<AdministrasiInternal> Update(
            Guid Uuid,
            AdministrasiInternal? prev,
            int PenelitianInternalId,
            string? judul,
            string? halamanSampul,
            string? halamanPengesahan,
            string? identitasPengusul,
            string? identitasMahasiswa,
            string? mitraKerjasama,
            string? luaranTargetCapaian,
            string? rab,
            string? latarBelakangRumusanMasalah,
            string? pendekatanPemecahanMasalah,
            string? sota,
            string? penjelasanCapaianRisetKepakaran,
            string? petaJalan,
            string? tahapanPenelitian,
            string? indikatorCapaianPadaRab,
            string? jadwal,
            string? daftarPustaka,
            string? biodataKetuaAnggota,
            string? paktaIntegritas,
            string? suratKetersediaanMitra,
            string keputusan,
            string? komentar
        )
        {
            if (prev == null)
            {
                return Result.Failure<AdministrasiInternal>(AdministrasiInternalErrors.NotFound(Uuid));
            }

            prev.PenelitianInternalId = PenelitianInternalId;
            prev.Judul = judul;
            prev.HalamanSampul = halamanSampul;
            prev.HalamanPengesahan = halamanPengesahan;
            prev.IdentitasPengusul = identitasPengusul;
            prev.IdentitasMahasiswa = identitasMahasiswa;
            prev.MitraKerjasama = mitraKerjasama;
            prev.LuaranTargetCapaian = luaranTargetCapaian;
            prev.Rab = rab;
            prev.LatarBelakangRumusanMasalah = latarBelakangRumusanMasalah;
            prev.PendekatanPemecahanMasalah = pendekatanPemecahanMasalah;
            prev.Sota = sota;
            prev.PenjelasanCapaianRisetKepakaran = penjelasanCapaianRisetKepakaran;
            prev.PetaJalan = petaJalan;
            prev.TahapanPenelitian = tahapanPenelitian;
            prev.IndikatorCapaianPadaRab = indikatorCapaianPadaRab;
            prev.Jadwal = jadwal;
            prev.DaftarPustaka = daftarPustaka;
            prev.BiodataKetuaAnggota = biodataKetuaAnggota;
            prev.PaktaIntegritas = paktaIntegritas;
            prev.SuratKetersediaanMitra = suratKetersediaanMitra;
            prev.Keputusan = keputusan;
            prev.Komentar = komentar;
            
            var emptyFields = prev.GetContentAttributes()
                                .Where(kv => string.IsNullOrWhiteSpace(kv.Value))
                                .Select(kv => kv.Key)
                                .ToList();

            var adaSesuaiFields = prev.GetContentAttributes()
                                .Where(kv => kv.Value == Options.AdaSesuai.ToString())
                                .Select(kv => kv.Key)
                                .ToList();

            var expectationKeputusan = adaSesuaiFields.Count != 21 ? Domain.Keputusan.PerluPerbaikan.ToString() : keputusan;

            if (emptyFields.Any() && emptyFields.Count < 21)
            {
                return Result.Failure<AdministrasiInternal>(AdministrasiInternalErrors.InvalidArgument());
            }
            if (adaSesuaiFields.Any() && keputusan != expectationKeputusan)
            {
                return Result.Failure<AdministrasiInternal>(AdministrasiInternalErrors.InvalidKeputusan());
            }

            return prev;
        }
    }
}
