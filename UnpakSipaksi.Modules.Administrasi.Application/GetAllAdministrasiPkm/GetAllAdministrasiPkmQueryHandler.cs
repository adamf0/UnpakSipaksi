using Dapper;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.Administrasi.Application.GetAdministrasiPkm;
using UnpakSipaksi.Modules.Administrasi.Domain.AdministrasiPkm;

namespace UnpakSipaksi.Modules.Administrasi.Application.GetAllAdministrasiPkm
{
    internal sealed class GetAllAdministrasiPkmQueryHandler(IDbConnectionFactory _dbConnectionFactory) : IQueryHandler<GetAllAdministrasiPkmQuery, List<AdministrasiPkmResponse>>
    {
        public async Task<Result<List<AdministrasiPkmResponse>>> Handle(GetAllAdministrasiPkmQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await _dbConnectionFactory.OpenConnectionAsync();

            const string sql =
            """
            SELECT 
                CAST(NULLIF(p.uuid, '') AS VARCHAR(36)) AS Uuid,
                a.level AS Level,
                a.judul AS Judul,
                a.halamanSampul AS HalamanSampul,
                a.halamanPengesahan AS HalamanPengesahan,
                a.identitasPengusul AS IdentitasPengusul,
                a.mitraPkm AS MitraPkm,
                a.luaranTargetCapaian AS LuaranTargetCapaian,
                a.rab AS Rab,
                a.ringkasanKataKunci AS RingkasanKataKunci,
                a.analisisSituasi AS AnalisisSituasi,
                a.permasalahanMitra AS PermasalahanMitra,
                a.solusiPermasalahan AS SolusiPermasalahan,
                a.metodePelaksanaan AS MetodePelaksanaan,
                a.jadwal AS Jadwal,
                a.daftarPustaka AS DaftarPustaka,
                a.biodataKetuaAnggota AS BiodataKetuaAnggota,
                a.gambaranIptek AS GambaranIptek,
                a.petaLokasiMitra AS PetaLokasiMitra,
                a.suratPernyataanKetuaPelaksana AS SuratPernyataanKetuaPelaksana,
                a.suratKetersediaanMitra AS SuratKetersediaanMitra,
                a.melibatkanMahasiswa AS MelibatkanMahasiswa,
                a.mendukungIKU AS MendukungIKU,
                a.keputusan AS Keputusan,
                a.komentar AS Komentar
            FROM pkm_administrasi a 
            JOIN pkm p ON a.id_pdp = p.id
            """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QueryAsync<AdministrasiPkmResponse>(sql);

            if (result == null || !result.Any())
            {
                return Result.Failure<List<AdministrasiPkmResponse>>(AdministrasiPkmErrors.EmptyData());
            }

            return Result.Success(result.ToList());
        }
    }
}
