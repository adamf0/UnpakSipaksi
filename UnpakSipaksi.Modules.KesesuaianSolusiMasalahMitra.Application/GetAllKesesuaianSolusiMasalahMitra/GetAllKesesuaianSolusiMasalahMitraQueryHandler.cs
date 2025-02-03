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
using UnpakSipaksi.Modules.KesesuaianSolusiMasalahMitra.Domain.KesesuaianSolusiMasalahMitra;
using UnpakSipaksi.Modules.KesesuaianSolusiMasalahMitra.Application.GetKesesuaianSolusiMasalahMitra;

namespace UnpakSipaksi.Modules.KesesuaianSolusiMasalahMitra.Application.GetAllKesesuaianSolusiMasalahMitra
{
    internal sealed class GetAllKesesuaianSolusiMasalahMitraQueryHandler(IDbConnectionFactory _dbConnectionFactory) : IQueryHandler<GetAllKesesuaianSolusiMasalahMitraQuery, List<KesesuaianSolusiMasalahMitraResponse>>
    {
        public async Task<Result<List<KesesuaianSolusiMasalahMitraResponse>>> Handle(GetAllKesesuaianSolusiMasalahMitraQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await _dbConnectionFactory.OpenConnectionAsync();

            const string sql =
            """
            SELECT 
                CAST(NULLIF(uuid, '') AS VARCHAR(36)) AS Uuid,
                 nama as Nama,
                 nilai as Nilai
            FROM kesesuaian_solusi_masalah_mitra
            """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QueryAsync<KesesuaianSolusiMasalahMitraResponse>(sql);

            if (result == null || !result.Any())
            {
                return Result.Failure<List<KesesuaianSolusiMasalahMitraResponse>>(KesesuaianSolusiMasalahMitraErrors.EmptyData());
            }

            return Result.Success(result.ToList());
        }
    }
}
