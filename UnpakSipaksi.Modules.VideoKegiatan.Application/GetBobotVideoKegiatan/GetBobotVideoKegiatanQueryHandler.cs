using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.VideoKegiatan.Domain.VideoKegiatan;

namespace UnpakSipaksi.Modules.VideoKegiatan.Application.GetBobotVideoKegiatan
{
    internal sealed class GetBobotVideoKegiatanQueryHandler(IDbConnectionFactory dbConnectionFactory)
        : IQueryHandler<GetBobotVideoKegiatanQuery, int?>
    {
        public async Task<Result<int?>> Handle(GetBobotVideoKegiatanQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            const string sql = """
                SELECT MAX(nilai)
                FROM video_kegiatan
            """;

            var nilaiList = (await connection.QueryAsync<int>(sql)).ToList();

            if (!nilaiList.Any())
            {
                return Result.Failure<int?>(VideoKegiatanErrors.EmptyData());
            }

            if (nilaiList.Count > 1)
            {
                return Result.Failure<int?>(VideoKegiatanErrors.NotSameValue());
            }

            return Result.Success(nilaiList?.First());
        }
    }
}
