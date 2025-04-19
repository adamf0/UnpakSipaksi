using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.ArtikelMediaMassa.Domain.ArtikelMediaMassa;

namespace UnpakSipaksi.Modules.ArtikelMediaMassa.Application.GetBobotArtikelMediaMassa
{
    internal sealed class GetBobotArtikelMediaMassaQueryHandler(IDbConnectionFactory dbConnectionFactory)
        : IQueryHandler<GetBobotArtikelMediaMassaQuery, int?>
    {
        public async Task<Result<int?>> Handle(GetBobotArtikelMediaMassaQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            const string sql = """
                SELECT MAX(nilai)
                FROM artikel_media_massa
            """;

            var nilaiList = (await connection.QueryAsync<int>(sql)).ToList();

            if (!nilaiList.Any())
            {
                return Result.Failure<int?>(ArtikelMediaMassaErrors.EmptyData());
            }

            if (nilaiList.Count > 1)
            {
                return Result.Failure<int?>(ArtikelMediaMassaErrors.NotSameValue());
            }

            return Result.Success(nilaiList?.First());
        }
    }
}
