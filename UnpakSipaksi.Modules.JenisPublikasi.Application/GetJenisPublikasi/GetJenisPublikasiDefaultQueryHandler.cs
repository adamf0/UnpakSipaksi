using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.JenisPublikasi.Domain.JenisPublikasi;

namespace UnpakSipaksi.Modules.JenisPublikasi.Application.GetJenisPublikasi
{
    internal sealed class GetJenisPublikasiDefaultQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetJenisPublikasiDefaultQuery, JenisPublikasiDefaultResponse>
    {
        public async Task<Result<JenisPublikasiDefaultResponse>> Handle(GetJenisPublikasiDefaultQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            //CAST(NULLIF(id_group, '') AS CHAR(36)) -> guid
            const string sql =
                $"""
                 SELECT 
                     id as Id,
                     CAST(NULLIF(uuid, '') AS VARCHAR(36)) AS Uuid,
                     nama as Nama,
                     sbu AS Sbu 
                 FROM jenis_publikasi 
                 WHERE uuid = @Uuid
                 """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QuerySingleOrDefaultAsync<JenisPublikasiDefaultResponse?>(sql, new { Uuid = request.JenisPublikasiUuid });
            if (result == null)
            {
                return Result.Failure<JenisPublikasiDefaultResponse>(JenisPublikasiErrors.NotFound(request.JenisPublikasiUuid));
            }

            return result;
        }
    }
}
