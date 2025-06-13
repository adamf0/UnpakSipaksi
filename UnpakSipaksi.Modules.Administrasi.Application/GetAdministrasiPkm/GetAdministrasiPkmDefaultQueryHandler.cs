using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.Administrasi.Domain.AdministrasiPkm;

namespace UnpakSipaksi.Modules.Administrasi.Application.GetAdministrasiPkm
{
    internal sealed class GetAdministrasiPkmDefaultQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetAdministrasiPkmDefaultQuery, AdministrasiPkmDefaultResponse>
    {
        public async Task<Result<AdministrasiPkmDefaultResponse>> Handle(GetAdministrasiPkmDefaultQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            //CAST(NULLIF(id_group, '') AS CHAR(36)) -> guid
            const string sql =
                $"""
                 SELECT 
                     a.id as Id,
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
                 WHERE p.uuid = @Uuid
                 """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QuerySingleOrDefaultAsync<AdministrasiPkmDefaultResponse?>(sql, new { Uuid = request.AdministrasiPkmUuid });
            if (result == null)
            {
                return Result.Failure<AdministrasiPkmDefaultResponse>(AdministrasiPkmErrors.NotFound(request.AdministrasiPkmUuid));
            }

            return result;
        }
    }
}
