using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.AuthorSinta.Domain.AuthorSinta;

namespace UnpakSipaksi.Modules.AuthorSinta.Application.GetAuthorSinta
{
    internal sealed class GetAuthorSintaQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetAuthorSintaQuery, AuthorSintaResponse>
    {
        public async Task<Result<AuthorSintaResponse>> Handle(GetAuthorSintaQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            //CAST(NULLIF(id_group, '') AS CHAR(36)) -> guid
            const string sql =
                $"""
                 SELECT 
                     CAST(NULLIF(uuid, '') AS VARCHAR(36)) AS Uuid,
                     NIDN as Nidn,
                     author_id AS AuthorId,
                     score AS Score 
                 FROM author_sinta
                 WHERE uuid = @Uuid
                 """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QuerySingleOrDefaultAsync<AuthorSintaResponse?>(sql, new { Uuid = request.AuthorSintaUuid });
            if (result == null)
            {
                return Result.Failure<AuthorSintaResponse>(AuthorSintaErrors.NotFound(request.AuthorSintaUuid));
            }

            return result;
        }
    }
}
