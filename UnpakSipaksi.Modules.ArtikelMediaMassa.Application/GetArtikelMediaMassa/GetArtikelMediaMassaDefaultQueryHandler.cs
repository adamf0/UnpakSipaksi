using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.ArtikelMediaMassa.Application.GetArtikelMediaMassa;
using UnpakSipaksi.Modules.ArtikelMediaMassa.Domain.ArtikelMediaMassa;

namespace UnpakSipaksi.Modules.ArtikelMediaMassa.Application.GetBobotArtikelMediaMassa
{
    internal sealed class GetArtikelMediaMassaDefaultQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetArtikelMediaMassaDefaultQuery, ArtikelMediaMassaDefaultResponse>
    {
        public async Task<Result<ArtikelMediaMassaDefaultResponse>> Handle(GetArtikelMediaMassaDefaultQuery request, CancellationToken cancellationToken)
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
                 FROM artikel_media_massa 
                 WHERE uuid = @Uuid
                 """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QuerySingleOrDefaultAsync<ArtikelMediaMassaDefaultResponse?>(sql, new { Uuid = request.ArtikelMediaMassaUuid });
            if (result == null)
            {
                return Result.Failure<ArtikelMediaMassaDefaultResponse>(ArtikelMediaMassaErrors.NotFound(request.ArtikelMediaMassaUuid));
            }

            return result;
        }
    }
}
