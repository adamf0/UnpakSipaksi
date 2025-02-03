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
using UnpakSipaksi.Modules.KesesuaianWaktuRabLuaranFasilitas.Domain.KesesuaianWaktuRabLuaranFasilitas;
using UnpakSipaksi.Modules.KesesuaianWaktuRabLuaranFasilitas.Application.GetKesesuaianWaktuRabLuaranFasilitas;

namespace UnpakSipaksi.Modules.KesesuaianWaktuRabLuaranFasilitas.Application.GetAllKesesuaianWaktuRabLuaranFasilitas
{
    internal sealed class GetAllKesesuaianWaktuRabLuaranFasilitasQueryHandler(IDbConnectionFactory _dbConnectionFactory) : IQueryHandler<GetAllKesesuaianWaktuRabLuaranFasilitasQuery, List<KesesuaianWaktuRabLuaranFasilitasResponse>>
    {
        public async Task<Result<List<KesesuaianWaktuRabLuaranFasilitasResponse>>> Handle(GetAllKesesuaianWaktuRabLuaranFasilitasQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await _dbConnectionFactory.OpenConnectionAsync();

            const string sql =
            """
            SELECT 
                CAST(NULLIF(uuid, '') AS VARCHAR(36)) AS Uuid,
                 name as Nama,
                 skor as Skor
            FROM kesesuaian_waktu_rab_luaran_fasilitas
            """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QueryAsync<KesesuaianWaktuRabLuaranFasilitasResponse>(sql);

            if (result == null || !result.Any())
            {
                return Result.Failure<List<KesesuaianWaktuRabLuaranFasilitasResponse>>(KesesuaianWaktuRabLuaranFasilitasErrors.EmptyData());
            }

            return Result.Success(result.ToList());
        }
    }
}
