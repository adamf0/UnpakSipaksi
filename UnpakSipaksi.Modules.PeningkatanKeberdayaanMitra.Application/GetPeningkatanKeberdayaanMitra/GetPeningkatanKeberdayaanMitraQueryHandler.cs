using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PeningkatanKeberdayaanMitra.Domain.PeningkatanKeberdayaanMitra;

namespace UnpakSipaksi.Modules.PeningkatanKeberdayaanMitra.Application.GetPeningkatanKeberdayaanMitra
{
    internal sealed class GetPeningkatanKeberdayaanMitraQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetPeningkatanKeberdayaanMitraQuery, PeningkatanKeberdayaanMitraResponse>
    {
        public async Task<Result<PeningkatanKeberdayaanMitraResponse>> Handle(GetPeningkatanKeberdayaanMitraQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            //CAST(NULLIF(id_group, '') AS CHAR(36)) -> guid
            const string sql =
                $"""
                 SELECT 
                     CAST(NULLIF(uuid, '') AS VARCHAR(36)) AS Uuid,
                     nama as Nama,
                     nilai as Nilai
                 FROM metode_rencana_kegiatan 
                 WHERE uuid = @Uuid
                 """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QuerySingleOrDefaultAsync<PeningkatanKeberdayaanMitraResponse?>(sql, new { Uuid = request.PeningkatanKeberdayaanMitraUuid });
            if (result == null)
            {
                return Result.Failure<PeningkatanKeberdayaanMitraResponse>(PeningkatanKeberdayaanMitraErrors.NotFound(request.PeningkatanKeberdayaanMitraUuid));
            }

            return result;
        }
    }
}
