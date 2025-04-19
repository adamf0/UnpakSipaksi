using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KejelasanPembagianTugasTim.Domain.KejelasanPembagianTugasTim;

namespace UnpakSipaksi.Modules.KejelasanPembagianTugasTim.Application.GetBobotKejelasanPembagianTugasTim
{
    internal sealed class GetBobotKejelasanPembagianTugasTimQueryHandler(IDbConnectionFactory dbConnectionFactory)
        : IQueryHandler<GetBobotKejelasanPembagianTugasTimQuery, int?>
    {
        public async Task<Result<int?>> Handle(GetBobotKejelasanPembagianTugasTimQuery request, CancellationToken cancellationToken)
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
                return Result.Failure<int?>(KejelasanPembagianTugasTimErrors.UnknownKategoriSkema());
            }

            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            var sql = $"""
                SELECT DISTINCT {column}
                FROM kejelasan_pembagian_tugas_tim
            """;

            var nilaiList = (await connection.QueryAsync<int>(sql)).ToList();

            if (!nilaiList.Any())
            {
                return Result.Failure<int?>(KejelasanPembagianTugasTimErrors.EmptyData());
            }

            if (nilaiList.Count > 1)
            {
                return Result.Failure<int?>(KejelasanPembagianTugasTimErrors.NotSameValue());
            }

            return Result.Success(nilaiList?.First());
        }
    }
}
