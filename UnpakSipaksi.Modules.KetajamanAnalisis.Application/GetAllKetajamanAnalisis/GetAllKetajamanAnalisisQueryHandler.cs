using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KetajamanAnalisis.Application.GetKetajamanAnalisis;
using UnpakSipaksi.Modules.KetajamanAnalisis.Domain.KetajamanAnalisis;

namespace UnpakSipaksi.Modules.KetajamanAnalisis.Application.GetAllKetajamanAnalisis
{
    internal sealed class GetAllKetajamanAnalisisQueryHandler(IDbConnectionFactory _dbConnectionFactory) : IQueryHandler<GetAllKetajamanAnalisisQuery, List<KetajamanAnalisisResponse>>
    {
        public async Task<Result<List<KetajamanAnalisisResponse>>> Handle(GetAllKetajamanAnalisisQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await _dbConnectionFactory.OpenConnectionAsync();

            const string sql =
            """
            SELECT 
                CAST(NULLIF(uuid, '') AS VARCHAR(36)) AS Uuid,
                nama as Nama,
                nilai AS Nilai
            FROM ketajaman_analisis
            """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QueryAsync<KetajamanAnalisisResponse>(sql);

            if (result == null || !result.Any())
            {
                return Result.Failure<List<KetajamanAnalisisResponse>>(KetajamanAnalisisErrors.EmptyData());
            }

            return Result.Success(result.ToList());
        }
    }
}
