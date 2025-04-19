using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PeningkatanKeberdayaanMitra.Domain.PeningkatanKeberdayaanMitra;

namespace UnpakSipaksi.Modules.PeningkatanKeberdayaanMitra.Application.GetPeningkatanKeberdayaanMitra
{
    internal sealed class GetPeningkatanKeberdayaanMitraDefaultQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetPeningkatanKeberdayaanMitraDefaultQuery, PeningkatanKeberdayaanMitraDefaultResponse>
    {
        public async Task<Result<PeningkatanKeberdayaanMitraDefaultResponse>> Handle(GetPeningkatanKeberdayaanMitraDefaultQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            //CAST(NULLIF(id_group, '') AS CHAR(36)) -> guid
            const string sql =
                $"""
                 SELECT 
                     id as Id,
                     CAST(NULLIF(uuid, '') AS VARCHAR(36)) AS Uuid,
                     name as Nama,
                     nilai AS Nilai 
                 FROM metode_rencana_kegiatan 
                 WHERE uuid = @Uuid
                 """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QuerySingleOrDefaultAsync<PeningkatanKeberdayaanMitraDefaultResponse?>(sql, new { Uuid = request.PeningkatanKeberdayaanMitraUuid });
            if (result == null)
            {
                return Result.Failure<PeningkatanKeberdayaanMitraDefaultResponse>(PeningkatanKeberdayaanMitraErrors.NotFound(request.PeningkatanKeberdayaanMitraUuid));
            }

            return result;
        }
    }
}
