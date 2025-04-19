using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.RelevansiProdukKepentinganNasional.Domain.RelevansiProdukKepentinganNasional;

namespace UnpakSipaksi.Modules.RelevansiProdukKepentinganNasional.Application.GetBobotRelevansiProdukKepentinganNasional
{
    internal sealed class GetBobotRelevansiProdukKepentinganNasionalQueryHandler(IDbConnectionFactory dbConnectionFactory)
        : IQueryHandler<GetBobotRelevansiProdukKepentinganNasionalQuery, int?>
    {
        public async Task<Result<int?>> Handle(GetBobotRelevansiProdukKepentinganNasionalQuery request, CancellationToken cancellationToken)
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
                return Result.Failure<int?>(RelevansiProdukKepentinganNasionalErrors.UnknownKategoriSkema());
            }

            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            var sql = $"""
                SELECT DISTINCT {column}
                FROM relevansi_produk_kepentingan_nasional
            """;

            var nilaiList = (await connection.QueryAsync<int>(sql)).ToList();

            if (!nilaiList.Any())
            {
                return Result.Failure<int?>(RelevansiProdukKepentinganNasionalErrors.EmptyData());
            }

            if (nilaiList.Count > 1)
            {
                return Result.Failure<int?>(RelevansiProdukKepentinganNasionalErrors.NotSameValue());
            }

            return Result.Success(nilaiList?.First());
        }
    }
}
