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
using UnpakSipaksi.Modules.ArtikelMediaMassa.Domain.ArtikelMediaMassa;
using UnpakSipaksi.Modules.ArtikelMediaMassa.Application.GetArtikelMediaMassa;

namespace UnpakSipaksi.Modules.ArtikelMediaMassa.Application.GetAllArtikelMediaMassa
{
    internal sealed class GetAllArtikelMediaMassaQueryHandler(IDbConnectionFactory _dbConnectionFactory) : IQueryHandler<GetAllArtikelMediaMassaQuery, List<ArtikelMediaMassaResponse>>
    {
        public async Task<Result<List<ArtikelMediaMassaResponse>>> Handle(GetAllArtikelMediaMassaQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await _dbConnectionFactory.OpenConnectionAsync();

            const string sql =
            """
            SELECT 
                CAST(NULLIF(uuid, '') AS VARCHAR(36)) AS Uuid,
                nama as Nama,
                nilai AS Nilai 
            FROM artikel_media_massa
            """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QueryAsync<ArtikelMediaMassaResponse>(sql);

            if (result == null || !result.Any())
            {
                return Result.Failure<List<ArtikelMediaMassaResponse>>(ArtikelMediaMassaErrors.EmptyData());
            }

            return Result.Success(result.ToList());
        }
    }
}
