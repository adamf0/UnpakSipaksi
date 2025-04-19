using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.AkurasiPenelitian.Domain.AkurasiPenelitian;

namespace UnpakSipaksi.Modules.AkurasiPenelitian.Application.GetAkurasiPenelitian
{
    internal sealed class GetAkurasiPenelitianQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetAkurasiPenelitianQuery, AkurasiPenelitianResponse>
    {
        public async Task<Result<AkurasiPenelitianResponse>> Handle(GetAkurasiPenelitianQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            //CAST(NULLIF(id_group, '') AS CHAR(36)) -> guid
            const string sql =
                $"""
                 SELECT 
                     CAST(NULLIF(uuid, '') AS VARCHAR(36)) AS Uuid,
                     name as Nama,
                     skor AS Skor 
                 FROM akurasi_penelitian 
                 WHERE uuid = @Uuid
                 """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QuerySingleOrDefaultAsync<AkurasiPenelitianResponse?>(sql, new { Uuid = request.AkurasiPenelitianUuid });
            if (result == null)
            {
                return Result.Failure<AkurasiPenelitianResponse>(AkurasiPenelitianErrors.NotFound(request.AkurasiPenelitianUuid));
            }

            return result;
        }
    }
}
