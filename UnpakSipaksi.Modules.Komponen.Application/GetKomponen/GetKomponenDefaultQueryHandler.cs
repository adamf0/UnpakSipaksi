using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.Komponen.Domain.Komponen;

namespace UnpakSipaksi.Modules.Komponen.Application.GetKomponen
{
    internal sealed class GetKomponenDefaultQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetKomponenDefaultQuery, KomponenDefaultResponse>
    {
        public async Task<Result<KomponenDefaultResponse>> Handle(GetKomponenDefaultQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            //CAST(NULLIF(id_group, '') AS CHAR(36)) -> guid
            const string sql =
                $"""
                 SELECT 
                     id as Id,
                     CAST(NULLIF(uuid, '') AS VARCHAR(36)) AS Uuid,
                     nama as Nama,
                     max_biaya AS MaxBiaya
                 FROM komponen
                 WHERE uuid = @Uuid
                 """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QuerySingleOrDefaultAsync<KomponenDefaultResponse?>(sql, new { Uuid = request.KomponenUuid });
            if (result == null)
            {
                return Result.Failure<KomponenDefaultResponse>(KomponenErrors.NotFound(request.KomponenUuid));
            }

            return result;
        }
    }
}
