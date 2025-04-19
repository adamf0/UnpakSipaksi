using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.InovasiPemecahanMasalah.Domain.InovasiPemecahanMasalah;

namespace UnpakSipaksi.Modules.InovasiPemecahanMasalah.Application.GetBobotInovasiPemecahanMasalah
{
    internal sealed class GetBobotInovasiPemecahanMasalahQueryHandler(IDbConnectionFactory dbConnectionFactory)
        : IQueryHandler<GetBobotInovasiPemecahanMasalahQuery, int?>
    {
        public async Task<Result<int?>> Handle(GetBobotInovasiPemecahanMasalahQuery request, CancellationToken cancellationToken)
        {
            string column = request.KategoriSkema switch
            {
                "Penelitian Dasar" => "bobot_pdp",
                "Penelitian Dosen Pemula (PDP)" => "bobot_penelitian_dasar",
                "Penelitian Terapan" => "bobot_terapan",
                "Penelitian Kolaborasi" => "bobot_kerjasama",
                _ => string.Empty,
            };
            if (string.IsNullOrEmpty(column))
            {
                return Result.Failure<int?>(InovasiPemecahanMasalahErrors.UnknownKategoriSkema());
            }

            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            var sql = $"""
                SELECT DISTINCT {column}
                FROM inovasi_pemecahan_masalah
            """;

            var nilaiList = (await connection.QueryAsync<int>(sql)).ToList();

            if (!nilaiList.Any())
            {
                return Result.Failure<int?>(InovasiPemecahanMasalahErrors.EmptyData());
            }

            if (nilaiList.Count > 1)
            {
                return Result.Failure<int?>(InovasiPemecahanMasalahErrors.NotSameValue());
            }

            return Result.Success(nilaiList?.First());
        }
    }
}
