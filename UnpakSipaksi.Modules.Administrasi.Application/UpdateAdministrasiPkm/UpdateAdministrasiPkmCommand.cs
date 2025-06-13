using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.Administrasi.Application.UpdateAdministrasiPkm
{
    public sealed record UpdateAdministrasiPkmCommand(
            string uuidPenelitianPkm,
            string Uuid,
            string level,
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
    ) : ICommand;
}
