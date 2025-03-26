using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.RumusanPrioritasMitra.Application.GetRumusanPrioritasMitra;
using UnpakSipaksi.Modules.RumusanPrioritasMitra.Domain.RumusanPrioritasMitra;

namespace UnpakSipaksi.Modules.RumusanPrioritasMitra.Application.GetAllRumusanPrioritasMitra
{
    internal sealed class GetAllRumusanPrioritasMitraQueryHandler(IDbConnectionFactory _dbConnectionFactory) : IQueryHandler<GetAllRumusanPrioritasMitraQuery, List<RumusanPrioritasMitraResponse>>
    {
        public async Task<Result<List<RumusanPrioritasMitraResponse>>> Handle(GetAllRumusanPrioritasMitraQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await _dbConnectionFactory.OpenConnectionAsync();

            const string sql =
            """
            SELECT 
                CAST(NULLIF(uuid, '') AS VARCHAR(36)) AS Uuid,
                 nama as Nama,
                 nilai as Nilai
            FROM rumusan_prioritas_mitra
            """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QueryAsync<RumusanPrioritasMitraResponse>(sql);

            if (result == null || !result.Any())
            {
                return Result.Failure<List<RumusanPrioritasMitraResponse>>(RumusanPrioritasMitraErrors.EmptyData());
            }

            return Result.Success(result.ToList());
        }
    }
}
