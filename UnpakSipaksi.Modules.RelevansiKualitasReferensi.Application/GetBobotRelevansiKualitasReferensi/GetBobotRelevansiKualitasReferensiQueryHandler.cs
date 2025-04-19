using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.RelevansiKualitasReferensi.Domain.RelevansiKualitasReferensi;

namespace UnpakSipaksi.Modules.RelevansiKualitasReferensi.Application.GetBobotRelevansiKualitasReferensi
{
    internal sealed class GetBobotRelevansiKualitasReferensiQueryHandler(IDbConnectionFactory dbConnectionFactory)
        : IQueryHandler<GetBobotRelevansiKualitasReferensiQuery, int?>
    {
        public async Task<Result<int?>> Handle(GetBobotRelevansiKualitasReferensiQuery request, CancellationToken cancellationToken)
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
                return Result.Failure<int?>(RelevansiKualitasReferensiErrors.UnknownKategoriSkema());
            }

            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            var sql = $"""
                SELECT DISTINCT {column}
                FROM relevansi_kualitas_referensi
            """;

            var nilaiList = (await connection.QueryAsync<int>(sql)).ToList();

            if (!nilaiList.Any())
            {
                return Result.Failure<int?>(RelevansiKualitasReferensiErrors.EmptyData());
            }

            if (nilaiList.Count > 1)
            {
                return Result.Failure<int?>(RelevansiKualitasReferensiErrors.NotSameValue());
            }

            return Result.Success(nilaiList?.First());
        }
    }
}
