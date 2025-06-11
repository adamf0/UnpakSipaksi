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
using UnpakSipaksi.Modules.IndikatorCapaian.Application.GetIndikatorCapaian;
using UnpakSipaksi.Modules.IndikatorCapaian.Domain;

namespace UnpakSipaksi.Modules.IndikatorCapaian.Application.GetAllIndikatorCapaian
{
    internal sealed class GetAllIndikatorCapaianQueryHandler(IDbConnectionFactory _dbConnectionFactory) : IQueryHandler<GetAllIndikatorCapaianQuery, List<IndikatorCapaianResponse>>
    {
        public async Task<Result<List<IndikatorCapaianResponse>>> Handle(GetAllIndikatorCapaianQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await _dbConnectionFactory.OpenConnectionAsync();

            const string sql =
            """
            SELECT 
                CAST(NULLIF(ic.uuid, '') AS VARCHAR(36)) AS Uuid,
                CAST(NULLIF(jl.uuid, '') AS VARCHAR(36)) AS UuidJenisLuaran,
                ic.nama as Nama,
                ic.status as Status
            FROM pkm_indikator_capaian ic 
            LEFT JOIN pkm_jenis_luaran jl ON ic.id_jenis_luaran = jl.id 
            """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QueryAsync<IndikatorCapaianResponse>(sql);

            if (result == null || !result.Any())
            {
                return Result.Failure<List<IndikatorCapaianResponse>>(IndikatorCapaianErrors.EmptyData());
            }

            return Result.Success(result.ToList());
        }
    }
}
