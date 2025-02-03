using Dapper;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.AuthorSinta.Domain.AuthorSinta;
using UnpakSipaksi.Modules.AuthorSinta.Application.GetAuthorSinta;

namespace UnpakSipaksi.Modules.AuthorSinta.Application.GetAllAuthorSinta
{
    internal sealed class GetAllAuthorSintaQueryHandler(IDbConnectionFactory _dbConnectionFactory) : IQueryHandler<GetAllAuthorSintaQuery, List<AuthorSintaResponse>>
    {
        public async Task<Result<List<AuthorSintaResponse>>> Handle(GetAllAuthorSintaQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await _dbConnectionFactory.OpenConnectionAsync();

            const string sql =
            """
            SELECT 
                CAST(NULLIF(uuid, '') AS VARCHAR(36)) AS Uuid,
                NIDN as Nidn,
                author_id AS AuthorId,
                score AS Score 
            FROM author_sinta
            """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QueryAsync<AuthorSintaResponse>(sql);

            if (result == null || !result.Any())
            {
                return Result.Failure<List<AuthorSintaResponse>>(AuthorSintaErrors.EmptyData());
            }

            return Result.Success(result.ToList());
        }
    }
}
