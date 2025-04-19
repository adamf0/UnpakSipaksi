using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.RumusanPrioritasMitra.Domain.RumusanPrioritasMitra;

namespace UnpakSipaksi.Modules.RumusanPrioritasMitra.Application.GetBobotRumusanPrioritasMitra
{
    internal sealed class GetBobotRumusanPrioritasMitraQueryHandler(IDbConnectionFactory dbConnectionFactory)
        : IQueryHandler<GetBobotRumusanPrioritasMitraQuery, int?>
    {
        public async Task<Result<int?>> Handle(GetBobotRumusanPrioritasMitraQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            const string sql = """
                SELECT MAX(nilai)
                FROM rumusan_prioritas_mitra
            """;

            var nilaiList = (await connection.QueryAsync<int>(sql)).ToList();

            if (!nilaiList.Any())
            {
                return Result.Failure<int?>(RumusanPrioritasMitraErrors.EmptyData());
            }

            if (nilaiList.Count > 1)
            {
                return Result.Failure<int?>(RumusanPrioritasMitraErrors.NotSameValue());
            }

            return Result.Success(nilaiList?.First());
        }
    }
}
