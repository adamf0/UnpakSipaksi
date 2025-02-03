using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KebaruanReferensi.Domain.KebaruanReferensi;

namespace UnpakSipaksi.Modules.KebaruanReferensi.Application.GetKebaruanReferensi
{
    internal sealed class GetKebaruanReferensiQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetKebaruanReferensiQuery, KebaruanReferensiResponse>
    {
        public async Task<Result<KebaruanReferensiResponse>> Handle(GetKebaruanReferensiQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            //CAST(NULLIF(id_group, '') AS CHAR(36)) -> guid
            const string sql =
                $"""
                 SELECT 
                     CAST(NULLIF(uuid, '') AS VARCHAR(36)) AS Uuid,
                     name as Nama,
                     skor as Skor
                 FROM kebaruan_referensi
                 WHERE uuid = @Uuid
                 """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QuerySingleOrDefaultAsync<KebaruanReferensiResponse?>(sql, new { Uuid = request.KebaruanReferensiUuid });
            if (result == null)
            {
                return Result.Failure<KebaruanReferensiResponse>(KebaruanReferensiErrors.NotFound(request.KebaruanReferensiUuid));
            }

            return result;
        }
    }
}
