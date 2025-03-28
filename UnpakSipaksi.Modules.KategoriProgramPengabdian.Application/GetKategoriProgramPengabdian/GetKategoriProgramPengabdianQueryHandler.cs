using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KategoriProgramPengabdian.Domain.KategoriProgramPengabdian;

namespace UnpakSipaksi.Modules.KategoriProgramPengabdian.Application.GetKategoriProgramPengabdian
{
    internal sealed class GetKategoriProgramPengabdianQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetKategoriProgramPengabdianQuery, KategoriProgramPengabdianResponse>
    {
        public async Task<Result<KategoriProgramPengabdianResponse>> Handle(GetKategoriProgramPengabdianQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            //CAST(NULLIF(id_group, '') AS CHAR(36)) -> guid
            const string sql =
                $"""
                 SELECT 
                     CAST(NULLIF(uuid, '') AS VARCHAR(36)) AS Uuid,
                     nama as Nama,
                     rule as Rule
                 FROM kategori_program_pengabdian 
                 WHERE uuid = @Uuid
                 """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QuerySingleOrDefaultAsync<KategoriProgramPengabdianResponse?>(sql, new { Uuid = request.KategoriProgramPengabdianUuid });
            if (result == null)
            {
                return Result.Failure<KategoriProgramPengabdianResponse>(KategoriProgramPengabdianErrors.NotFound(request.KategoriProgramPengabdianUuid));
            }

            return result;
        }
    }
}
