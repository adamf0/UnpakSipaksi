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
using UnpakSipaksi.Modules.KejelasanPembagianTugasTim.Domain.KejelasanPembagianTugasTim;
using UnpakSipaksi.Modules.KejelasanPembagianTugasTim.Application.GetKejelasanPembagianTugasTim;

namespace UnpakSipaksi.Modules.KejelasanPembagianTugasTim.Application.GetAllKejelasanPembagianTugasTim
{
    internal sealed class GetAllKejelasanPembagianTugasTimQueryHandler(IDbConnectionFactory _dbConnectionFactory) : IQueryHandler<GetAllKejelasanPembagianTugasTimQuery, List<KejelasanPembagianTugasTimResponse>>
    {
        public async Task<Result<List<KejelasanPembagianTugasTimResponse>>> Handle(GetAllKejelasanPembagianTugasTimQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await _dbConnectionFactory.OpenConnectionAsync();

            const string sql =
            """
            SELECT 
                CAST(NULLIF(uuid, '') AS VARCHAR(36)) AS Uuid,
                name as Nama,
                skor as Skor
            FROM kejelasan_pembagian_tugas_tim
            """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QueryAsync<KejelasanPembagianTugasTimResponse>(sql);

            if (result == null || !result.Any())
            {
                return Result.Failure<List<KejelasanPembagianTugasTimResponse>>(KejelasanPembagianTugasTimErrors.EmptyData());
            }

            return Result.Success(result.ToList());
        }
    }
}
