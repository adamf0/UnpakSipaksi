using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.Administrasi.Application.GetAdministrasiInternal
{
    public sealed record AdministrasiInternalDefaultResponse
    {
        public string Id { get; set; }
        public string Uuid { get; set; }
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
    }
}
