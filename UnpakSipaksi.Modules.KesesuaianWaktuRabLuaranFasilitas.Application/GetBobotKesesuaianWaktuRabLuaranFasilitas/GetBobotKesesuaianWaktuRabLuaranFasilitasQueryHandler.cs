using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KesesuaianWaktuRabLuaranFasilitas.Domain.KesesuaianWaktuRabLuaranFasilitas;

namespace UnpakSipaksi.Modules.KesesuaianWaktuRabLuaranFasilitas.Application.GetBobotKesesuaianWaktuRabLuaranFasilitas
{
    internal sealed class GetBobotKesesuaianWaktuRabLuaranFasilitasQueryHandler(IDbConnectionFactory dbConnectionFactory)
        : IQueryHandler<GetBobotKesesuaianWaktuRabLuaranFasilitasQuery, int?>
    {
        public async Task<Result<int?>> Handle(GetBobotKesesuaianWaktuRabLuaranFasilitasQuery request, CancellationToken cancellationToken)
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
                return Result.Failure<int?>(KesesuaianWaktuRabLuaranFasilitasErrors.UnknownKategoriSkema());
            }

            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            var sql = $"""
                SELECT DISTINCT {column}
                FROM kesesuaian_waktu_rab_luaran_fasilitas
            """;

            var nilaiList = (await connection.QueryAsync<int>(sql)).ToList();

            if (!nilaiList.Any())
            {
                return Result.Failure<int?>(KesesuaianWaktuRabLuaranFasilitasErrors.EmptyData());
            }

            if (nilaiList.Count > 1)
            {
                return Result.Failure<int?>(KesesuaianWaktuRabLuaranFasilitasErrors.NotSameValue());
            }

            return Result.Success(nilaiList?.First());
        }
    }
}
