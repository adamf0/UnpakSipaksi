using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.RelevansiProdukKepentinganNasional.Domain.RelevansiProdukKepentinganNasional;

namespace UnpakSipaksi.Modules.RelevansiProdukKepentinganNasional.Application.GetRelevansiProdukKepentinganNasional
{
    internal sealed class GetRelevansiProdukKepentinganNasionalQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetRelevansiProdukKepentinganNasionalQuery, RelevansiProdukKepentinganNasionalResponse>
    {
        public async Task<Result<RelevansiProdukKepentinganNasionalResponse>> Handle(GetRelevansiProdukKepentinganNasionalQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            //CAST(NULLIF(id_group, '') AS CHAR(36)) -> guid
            const string sql =
                $"""
                 SELECT 
                     CAST(NULLIF(uuid, '') AS VARCHAR(36)) AS Uuid,
                     name as Nama,
                     skor AS Skor 
                 FROM relevansi_produk_kepentingan_nasional 
                 WHERE uuid = @Uuid
                 """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QuerySingleOrDefaultAsync<RelevansiProdukKepentinganNasionalResponse?>(sql, new { Uuid = request.Uuid });
            if (result == null)
            {
                return Result.Failure<RelevansiProdukKepentinganNasionalResponse>(RelevansiProdukKepentinganNasionalErrors.NotFound(request.Uuid));
            }

            return result;
        }
    }
}
