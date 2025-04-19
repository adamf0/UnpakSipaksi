using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KesesuaianTkt.Domain.KesesuaianTkt;

namespace UnpakSipaksi.Modules.KesesuaianTkt.Application.GetBobotKesesuaianTkt
{
    internal sealed class GetBobotKesesuaianTktQueryHandler(IDbConnectionFactory dbConnectionFactory)
        : IQueryHandler<GetBobotKesesuaianTktQuery, int?>
    {
        public async Task<Result<int?>> Handle(GetBobotKesesuaianTktQuery request, CancellationToken cancellationToken)
        {
            string column = request.KategoriSkema switch
            {
                "Penelitian Dasar" => "bobot_pdp",
                "Penelitian Dosen Pemula (PDP)" => "bobot_penelitian_dasar",
                "Penelitian Terapan" => "bobot_terapan",
                "Penelitian Kolaborasi" => "bobot_kerjasama",
                _ => string.Empty,
            };
            if (string.IsNullOrEmpty(column))
            {
                return Result.Failure<int?>(KesesuaianTktErrors.UnknownKategoriSkema());
            }

            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            var sql = $"""
                SELECT DISTINCT {column}
                FROM kesesuaian_tkt
            """;

            var nilaiList = (await connection.QueryAsync<int>(sql)).ToList();

            if (!nilaiList.Any())
            {
                return Result.Failure<int?>(KesesuaianTktErrors.EmptyData());
            }

            if (nilaiList.Count > 1)
            {
                return Result.Failure<int?>(KesesuaianTktErrors.NotSameValue());
            }

            return Result.Success(nilaiList?.First());
        }
    }
}
