using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KetajamanAnalisis.Domain.KetajamanAnalisis;

namespace UnpakSipaksi.Modules.KetajamanAnalisis.Application.GetBobotKetajamanAnalisis
{
    internal sealed class GetBobotKetajamanAnalisisQueryHandler(IDbConnectionFactory dbConnectionFactory)
        : IQueryHandler<GetBobotKetajamanAnalisisQuery, int?>
    {
        public async Task<Result<int?>> Handle(GetBobotKetajamanAnalisisQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            const string sql = """
                SELECT MAX(nilai)
                FROM ketajaman_analisis
            """;

            var nilaiList = (await connection.QueryAsync<int>(sql)).ToList();

            if (!nilaiList.Any())
            {
                return Result.Failure<int?>(KetajamanAnalisisErrors.EmptyData());
            }

            if (nilaiList.Count > 1)
            {
                return Result.Failure<int?>(KetajamanAnalisisErrors.NotSameValue());
            }

            return Result.Success(nilaiList?.First());
        }
    }
}
