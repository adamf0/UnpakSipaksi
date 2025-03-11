using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KualitasKuantitasPublikasiJurnalIlmiah.Domain.KualitasKuantitasPublikasiJurnalIlmiah;

namespace UnpakSipaksi.Modules.KualitasKuantitasPublikasiJurnalIlmiah.Application.GetKualitasKuantitasPublikasiJurnalIlmiah
{
    internal sealed class GetKualitasKuantitasPublikasiJurnalIlmiahQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetKualitasKuantitasPublikasiJurnalIlmiahQuery, KualitasKuantitasPublikasiJurnalIlmiahResponse>
    {
        public async Task<Result<KualitasKuantitasPublikasiJurnalIlmiahResponse>> Handle(GetKualitasKuantitasPublikasiJurnalIlmiahQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            //CAST(NULLIF(id_group, '') AS CHAR(36)) -> guid
            const string sql =
                $"""
                 SELECT 
                     CAST(NULLIF(uuid, '') AS VARCHAR(36)) AS Uuid,
                     nama as Nama,
                     nilai as Nilai
                 FROM kualitas_kuantitas_publikasi_jurnal_ilmiah 
                 WHERE uuid = @Uuid
                 """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QuerySingleOrDefaultAsync<KualitasKuantitasPublikasiJurnalIlmiahResponse?>(sql, new { Uuid = request.KualitasIpteksUuid });
            if (result == null)
            {
                return Result.Failure<KualitasKuantitasPublikasiJurnalIlmiahResponse>(KualitasKuantitasPublikasiJurnalIlmiahErrors.NotFound(request.KualitasIpteksUuid));
            }

            return result;
        }
    }
}
