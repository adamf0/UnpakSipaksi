using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.InovasiPemecahanMasalah.Domain.InovasiPemecahanMasalah;

namespace UnpakSipaksi.Modules.InovasiPemecahanMasalah.Application.GetInovasiPemecahanMasalah
{
    internal sealed class GetInovasiPemecahanMasalahQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetInovasiPemecahanMasalahQuery, InovasiPemecahanMasalahResponse>
    {
        public async Task<Result<InovasiPemecahanMasalahResponse>> Handle(GetInovasiPemecahanMasalahQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            //CAST(NULLIF(id_group, '') AS CHAR(36)) -> guid
            const string sql =
                $"""
                 SELECT 
                     CAST(NULLIF(uuid, '') AS VARCHAR(36)) AS Uuid,
                     name as Nama,
                     skor AS Skor 
                 FROM inovasi_pemecahan_masalah 
                 WHERE uuid = @Uuid
                 """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QuerySingleOrDefaultAsync<InovasiPemecahanMasalahResponse?>(sql, new { Uuid = request.InovasiPemecahanMasalahUuid });
            if (result == null)
            {
                return Result.Failure<InovasiPemecahanMasalahResponse>(InovasiPemecahanMasalahErrors.NotFound(request.InovasiPemecahanMasalahUuid));
            }

            return result;
        }
    }
}
