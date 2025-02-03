using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.ArtikelMediaMassa.Domain.ArtikelMediaMassa;

namespace UnpakSipaksi.Modules.ArtikelMediaMassa.Application.GetArtikelMediaMassa
{
    internal sealed class GetArtikelMediaMassaQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetArtikelMediaMassaQuery, ArtikelMediaMassaResponse>
    {
        public async Task<Result<ArtikelMediaMassaResponse>> Handle(GetArtikelMediaMassaQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            //CAST(NULLIF(id_group, '') AS CHAR(36)) -> guid
            const string sql =
                $"""
                 SELECT 
                     CAST(NULLIF(uuid, '') AS VARCHAR(36)) AS Uuid,
                     nama as Nama,
                     nilai AS Nilai 
                 FROM artikel_media_massa 
                 WHERE uuid = @Uuid
                 """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QuerySingleOrDefaultAsync<ArtikelMediaMassaResponse?>(sql, new { Uuid = request.ArtikelMediaMassaUuid });
            if (result == null)
            {
                return Result.Failure<ArtikelMediaMassaResponse>(ArtikelMediaMassaErrors.NotFound(request.ArtikelMediaMassaUuid));
            }

            return result;
        }
    }
}
