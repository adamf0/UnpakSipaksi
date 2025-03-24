using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PeningkatanKeberdayaanMitra.Application.GetPeningkatanKeberdayaanMitra;
using UnpakSipaksi.Modules.PeningkatanKeberdayaanMitra.Domain.PeningkatanKeberdayaanMitra;

namespace UnpakSipaksi.Modules.PeningkatanKeberdayaanMitra.Application.GetAllPeningkatanKeberdayaanMitra
{
    internal sealed class GetAllPeningkatanKeberdayaanMitraQueryHandler(IDbConnectionFactory _dbConnectionFactory) : IQueryHandler<GetAllPeningkatanKeberdayaanMitraQuery, List<PeningkatanKeberdayaanMitraResponse>>
    {
        public async Task<Result<List<PeningkatanKeberdayaanMitraResponse>>> Handle(GetAllPeningkatanKeberdayaanMitraQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await _dbConnectionFactory.OpenConnectionAsync();

            const string sql =
            """
            SELECT 
                CAST(NULLIF(uuid, '') AS VARCHAR(36)) AS Uuid,
                 nama as Nama,
                 nilai as Nilai
            FROM metode_rencana_kegiatan
            """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QueryAsync<PeningkatanKeberdayaanMitraResponse>(sql);

            if (result == null || !result.Any())
            {
                return Result.Failure<List<PeningkatanKeberdayaanMitraResponse>>(PeningkatanKeberdayaanMitraErrors.EmptyData());
            }

            return Result.Success(result.ToList());
        }
    }
}
