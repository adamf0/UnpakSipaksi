using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KejelasanPembagianTugasTim.Domain.KejelasanPembagianTugasTim;

namespace UnpakSipaksi.Modules.KejelasanPembagianTugasTim.Application.GetKejelasanPembagianTugasTim
{
    internal sealed class GetKejelasanPembagianTugasTimQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetKejelasanPembagianTugasTimQuery, KejelasanPembagianTugasTimResponse>
    {
        public async Task<Result<KejelasanPembagianTugasTimResponse>> Handle(GetKejelasanPembagianTugasTimQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            //CAST(NULLIF(id_group, '') AS CHAR(36)) -> guid
            const string sql =
                $"""
                 SELECT 
                     CAST(NULLIF(uuid, '') AS VARCHAR(36)) AS Uuid,
                     name as Nama,
                     skor as Skor
                 FROM kejelasan_pembagian_tugas_tim
                 WHERE uuid = @Uuid
                 """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QuerySingleOrDefaultAsync<KejelasanPembagianTugasTimResponse?>(sql, new { Uuid = request.KejelasanPembagianTugasTimUuid });
            if (result == null)
            {
                return Result.Failure<KejelasanPembagianTugasTimResponse>(KejelasanPembagianTugasTimErrors.NotFound(request.KejelasanPembagianTugasTimUuid));
            }

            return result;
        }
    }
}
