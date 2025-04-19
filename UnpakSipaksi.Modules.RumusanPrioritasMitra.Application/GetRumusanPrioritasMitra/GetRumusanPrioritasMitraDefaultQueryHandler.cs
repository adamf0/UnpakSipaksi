using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.RumusanPrioritasMitra.Domain.RumusanPrioritasMitra;

namespace UnpakSipaksi.Modules.RumusanPrioritasMitra.Application.GetRumusanPrioritasMitra
{
    internal sealed class GetRumusanPrioritasMitraDefaultQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetRumusanPrioritasMitraDefaultQuery, RumusanPrioritasMitraDefaultResponse>
    {
        public async Task<Result<RumusanPrioritasMitraDefaultResponse>> Handle(GetRumusanPrioritasMitraDefaultQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            //CAST(NULLIF(id_group, '') AS CHAR(36)) -> guid
            const string sql =
                $"""
                 SELECT 
                     id as Id,
                     CAST(NULLIF(uuid, '') AS VARCHAR(36)) AS Uuid,
                     name as Nama,
                     nilai AS Nilai 
                 FROM rumusan_prioritas_mitra 
                 WHERE uuid = @Uuid
                 """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QuerySingleOrDefaultAsync<RumusanPrioritasMitraDefaultResponse?>(sql, new { Uuid = request.RumusanPrioritasMitraUuid });
            if (result == null)
            {
                return Result.Failure<RumusanPrioritasMitraDefaultResponse>(RumusanPrioritasMitraErrors.NotFound(request.RumusanPrioritasMitraUuid));
            }

            return result;
        }
    }
}
