using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KualitasKuantitasPublikasiJurnalIlmiah.Domain.KualitasKuantitasPublikasiJurnalIlmiah;

namespace UnpakSipaksi.Modules.KualitasKuantitasPublikasiJurnalIlmiah.Application.GetKualitasKuantitasPublikasiJurnalIlmiah
{
    internal sealed class GetKualitasKuantitasPublikasiJurnalIlmiahDefaultQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetKualitasKuantitasPublikasiJurnalIlmiahDefaultQuery, KualitasKuantitasPublikasiJurnalIlmiahDefaultResponse>
    {
        public async Task<Result<KualitasKuantitasPublikasiJurnalIlmiahDefaultResponse>> Handle(GetKualitasKuantitasPublikasiJurnalIlmiahDefaultQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            //CAST(NULLIF(id_group, '') AS CHAR(36)) -> guid
            const string sql =
                $"""
                 SELECT 
                     id as Id,
                     CAST(NULLIF(uuid, '') AS VARCHAR(36)) AS Uuid,
                     name as Nama,
                     nilai AS Nilai 
                 FROM kualitas_kuantitas_publikasi_jurnal_ilmiah 
                 WHERE uuid = @Uuid
                 """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QuerySingleOrDefaultAsync<KualitasKuantitasPublikasiJurnalIlmiahDefaultResponse?>(sql, new { Uuid = request.KualitasKuantitasPublikasiJurnalIlmiahUuid });
            if (result == null)
            {
                return Result.Failure<KualitasKuantitasPublikasiJurnalIlmiahDefaultResponse>(KualitasKuantitasPublikasiJurnalIlmiahErrors.NotFound(request.KualitasKuantitasPublikasiJurnalIlmiahUuid));
            }

            return result;
        }
    }
}
