using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.Administrasi.Application.GetAdministrasiPkm
{
    public sealed record AdministrasiPkmDefaultResponse
    {
        public string Id { get; set; }
        public string Uuid { get; set; }
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
    }
}
