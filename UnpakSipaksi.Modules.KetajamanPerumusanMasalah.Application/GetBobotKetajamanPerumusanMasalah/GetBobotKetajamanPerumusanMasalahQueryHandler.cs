using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KetajamanPerumusanMasalah.Domain.KetajamanPerumusanMasalah;

namespace UnpakSipaksi.Modules.KetajamanPerumusanMasalah.Application.GetBobotKetajamanPerumusanMasalah
{
    internal sealed class GetBobotKetajamanPerumusanMasalahQueryHandler(IDbConnectionFactory dbConnectionFactory)
        : IQueryHandler<GetBobotKetajamanPerumusanMasalahQuery, int?>
    {
        public async Task<Result<int?>> Handle(GetBobotKetajamanPerumusanMasalahQuery request, CancellationToken cancellationToken)
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
                return Result.Failure<int?>(KetajamanPerumusanMasalahErrors.UnknownKategoriSkema());
            }

            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            var sql = $"""
                SELECT DISTINCT {column}
                FROM ketajaman_perumusan_masalah
            """;

            var nilaiList = (await connection.QueryAsync<int>(sql)).ToList();

            if (!nilaiList.Any())
            {
                return Result.Failure<int?>(KetajamanPerumusanMasalahErrors.EmptyData());
            }

            if (nilaiList.Count > 1)
            {
                return Result.Failure<int?>(KetajamanPerumusanMasalahErrors.NotSameValue());
            }

            return Result.Success(nilaiList?.First());
        }
    }
}
