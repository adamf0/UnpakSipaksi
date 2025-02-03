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
using UnpakSipaksi.Modules.JenisPublikasi.Domain.JenisPublikasi;
using UnpakSipaksi.Modules.JenisPublikasi.Application.GetJenisPublikasi;

namespace UnpakSipaksi.Modules.JenisPublikasi.Application.GetAllJenisPublikasi
{
    internal sealed class GetAllJenisPublikasiQueryHandler(IDbConnectionFactory _dbConnectionFactory) : IQueryHandler<GetAllJenisPublikasiQuery, List<JenisPublikasiResponse>>
    {
        public async Task<Result<List<JenisPublikasiResponse>>> Handle(GetAllJenisPublikasiQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await _dbConnectionFactory.OpenConnectionAsync();

            const string sql =
            """
            SELECT 
                CAST(NULLIF(uuid, '') AS VARCHAR(36)) AS Uuid,
                nama as Nama,
                sbu AS Sbu 
            FROM jenis_publikasi
            """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QueryAsync<JenisPublikasiResponse>(sql);

            if (result == null || !result.Any())
            {
                return Result.Failure<List<JenisPublikasiResponse>>(JenisPublikasiErrors.EmptyData());
            }

            return Result.Success(result.ToList());
        }
    }
}
