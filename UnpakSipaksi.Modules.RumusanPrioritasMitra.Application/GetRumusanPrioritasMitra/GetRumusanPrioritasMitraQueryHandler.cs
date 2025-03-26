using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.RumusanPrioritasMitra.Domain.RumusanPrioritasMitra;

namespace UnpakSipaksi.Modules.RumusanPrioritasMitra.Application.GetRumusanPrioritasMitra
{
    internal sealed class GetRumusanPrioritasMitraQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetRumusanPrioritasMitraQuery, RumusanPrioritasMitraResponse>
    {
        public async Task<Result<RumusanPrioritasMitraResponse>> Handle(GetRumusanPrioritasMitraQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            //CAST(NULLIF(id_group, '') AS CHAR(36)) -> guid
            const string sql =
                $"""
                 SELECT 
                     CAST(NULLIF(uuid, '') AS VARCHAR(36)) AS Uuid,
                     nama as Nama,
                     nilai as Nilai
                 FROM rumusan_prioritas_mitra 
                 WHERE uuid = @Uuid
                 """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QuerySingleOrDefaultAsync<RumusanPrioritasMitraResponse?>(sql, new { Uuid = request.RumusanPrioritasMitraUuid });
            if (result == null)
            {
                return Result.Failure<RumusanPrioritasMitraResponse>(RumusanPrioritasMitraErrors.NotFound(request.RumusanPrioritasMitraUuid));
            }

            return result;
        }
    }
}
