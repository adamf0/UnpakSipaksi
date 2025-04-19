using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PeningkatanKeberdayaanMitra.Domain.PeningkatanKeberdayaanMitra;

namespace UnpakSipaksi.Modules.PeningkatanKeberdayaanMitra.Application.GetBobotPeningkatanKeberdayaanMitra
{
    internal sealed class GetBobotPeningkatanKeberdayaanMitraQueryHandler(IDbConnectionFactory dbConnectionFactory)
        : IQueryHandler<GetBobotPeningkatanKeberdayaanMitraQuery, int?>
    {
        public async Task<Result<int?>> Handle(GetBobotPeningkatanKeberdayaanMitraQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            const string sql = """
                SELECT MAX(nilai)
                FROM metode_rencana_kegiatan
            """;

            var nilaiList = (await connection.QueryAsync<int>(sql)).ToList();

            if (!nilaiList.Any())
            {
                return Result.Failure<int?>(PeningkatanKeberdayaanMitraErrors.EmptyData());
            }

            if (nilaiList.Count > 1)
            {
                return Result.Failure<int?>(PeningkatanKeberdayaanMitraErrors.NotSameValue());
            }

            return Result.Success(nilaiList?.First());
        }
    }
}
