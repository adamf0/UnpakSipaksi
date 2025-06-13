using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.Administrasi.Application.CreateAdministrasiInternal
{
    public sealed record CreateAdministrasiInternalCommand(
            string uuidPenelitianInternal,
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
    ) : ICommand<Guid>;
}
