using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KategoriMitraPenelitian.Domain.KategoriMitraPenelitian;

namespace UnpakSipaksi.Modules.KategoriMitraPenelitian.Application.GetKategoriMitraPenelitian
{
    internal sealed class GetKategoriMitraPenelitianQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetKategoriMitraPenelitianQuery, KategoriMitraPenelitianResponse>
    {
        public async Task<Result<KategoriMitraPenelitianResponse>> Handle(GetKategoriMitraPenelitianQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            //CAST(NULLIF(id_group, '') AS CHAR(36)) -> guid
            const string sql =
                $"""
                 SELECT 
                     CAST(NULLIF(uuid, '') AS VARCHAR(36)) AS Uuid,
                     nama as Nama
                 FROM kategori_mitra_penelitianpkmmandiri 
                 WHERE uuid = @Uuid
                 """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QuerySingleOrDefaultAsync<KategoriMitraPenelitianResponse?>(sql, new { Uuid = request.KategoriMitraPenelitianUuid });
            if (result == null)
            {
                return Result.Failure<KategoriMitraPenelitianResponse>(KategoriMitraPenelitianErrors.NotFound(request.KategoriMitraPenelitianUuid));
            }

            return result;
        }
    }
}
