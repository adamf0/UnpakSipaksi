using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KetajamanAnalisis.Domain.KetajamanAnalisis;

namespace UnpakSipaksi.Modules.KetajamanAnalisis.Application.GetKetajamanAnalisis
{
    internal sealed class GetKetajamanAnalisisQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetKetajamanAnalisisQuery, KetajamanAnalisisResponse>
    {
        public async Task<Result<KetajamanAnalisisResponse>> Handle(GetKetajamanAnalisisQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            //CAST(NULLIF(id_group, '') AS CHAR(36)) -> guid
            const string sql =
                $"""
                 SELECT 
                     CAST(NULLIF(uuid, '') AS VARCHAR(36)) AS Uuid,
                     nama as Nama,
                     nilai AS Nilai
                 FROM ketajaman_analisis 
                 WHERE uuid = @Uuid
                 """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QuerySingleOrDefaultAsync<KetajamanAnalisisResponse?>(sql, new { Uuid = request.KetajamanAnalisisUuid });
            if (result == null)
            {
                return Result.Failure<KetajamanAnalisisResponse>(KetajamanAnalisisErrors.NotFound(request.KetajamanAnalisisUuid));
            }

            return result;
        }
    }
}
