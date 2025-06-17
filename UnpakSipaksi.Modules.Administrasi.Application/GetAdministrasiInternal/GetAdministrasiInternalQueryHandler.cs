using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.Administrasi.Application.GetAdministrasiPkm;
using UnpakSipaksi.Modules.Administrasi.Domain.AdministrasiInternal;

namespace UnpakSipaksi.Modules.Administrasi.Application.GetAdministrasiInternal
{
    internal sealed class GetAdministrasiInternalQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetAdministrasiInternalQuery, AdministrasiInternalResponse>
    {
        public async Task<Result<AdministrasiInternalResponse>> Handle(GetAdministrasiInternalQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            //CAST(NULLIF(id_group, '') AS CHAR(36)) -> guid
            const string sql =
                $"""
                 SELECT 
                     CAST(NULLIF(p.uuid, '') AS VARCHAR(36)) AS Uuid,
                     a.level AS Level,
                     a.judul AS Judul,
                     a.halamanSampul AS HalamanSampul,
                     a.halamanPengesahan AS HalamanPengesahan,
                     a.identitasPengusul AS IdentitasPengusul,
                     a.identitasMahasiswa AS IdentitasMahasiswa,
                     a.mitraKerjasama AS MitraKerjasama,
                     a.luaranTargetCapaian AS LuaranTargetCapaian,
                     a.rab AS Rab,
                     a.latarBelakangRumusanMasalah AS LatarBelakangRumusanMasalah,
                     a.pendekatanPemecahanMasalah AS PendekatanPemecahanMasalah,
                     a.sota AS Sota,
                     a.penjelasanCapaianRisetKepakaran AS PenjelasanCapaianRisetKepakaran,
                     a.petaJalan AS PetaJalan,
                     a.tahapanPenelitian AS TahapanPenelitian,
                     a.indikatorCapaianPadaRab AS IndikatorCapaianPadaRab,
                     a.jadwal AS Jadwal,
                     a.daftarPustaka AS DaftarPustaka,
                     a.biodataKetuaAnggota AS BiodataKetuaAnggota,
                     a.paktaIntegritas AS PaktaIntegritas,
                     a.suratKetersediaanMitra AS SuratKetersediaanMitra,
                     a.keputusan AS Keputusan,
                     a.komentar AS Komentar
                 FROM penelitian_internal_administrasi a 
                 JOIN penelitian_internal p ON a.id_pdp = p.id
                 WHERE a.uuid = @Uuid and p.uuid = @UuidPenelitianPkm
                 """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QuerySingleOrDefaultAsync<AdministrasiInternalResponse?>(sql, new { Uuid = request.AdministrasiInternalUuid, UuidPenelitianPkm = request.UuidPenelitianHibah });
            if (result == null)
            {
                return Result.Failure<AdministrasiInternalResponse>(AdministrasiInternalErrors.NotFound(request.AdministrasiInternalUuid));
            }

            return result;
        }
    }
}
