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
using UnpakSipaksi.Modules.Administrasi.Application.GetAdministrasiInternal;
using UnpakSipaksi.Modules.Administrasi.Domain.AdministrasiInternal;

namespace UnpakSipaksi.Modules.Administrasi.Application.GetAllAdministrasiInternal
{
    internal sealed class GetAllAdministrasiInternalQueryHandler(IDbConnectionFactory _dbConnectionFactory) : IQueryHandler<GetAllAdministrasiInternalQuery, List<AdministrasiInternalResponse>>
    {
        public async Task<Result<List<AdministrasiInternalResponse>>> Handle(GetAllAdministrasiInternalQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await _dbConnectionFactory.OpenConnectionAsync();

            const string sql =
            """
            SELECT 
                a.id as Id,
                CAST(NULLIF(p.uuid, '') AS VARCHAR(36)) AS Uuid,
                a.level AS Level,
                a.judul AS Judul,
                a.halamanSampul AS HalamanSampul,
                a.halamanPengesahan AS HalamanPengesahan,
                a.identitasPengusul AS IdentitasPengusul,
                a.identitasMahasiswaAS IdentitasMahasiswa,
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
            """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QueryAsync<AdministrasiInternalResponse>(sql);

            if (result == null || !result.Any())
            {
                return Result.Failure<List<AdministrasiInternalResponse>>(AdministrasiInternalErrors.EmptyData());
            }

            return Result.Success(result.ToList());
        }
    }
}
