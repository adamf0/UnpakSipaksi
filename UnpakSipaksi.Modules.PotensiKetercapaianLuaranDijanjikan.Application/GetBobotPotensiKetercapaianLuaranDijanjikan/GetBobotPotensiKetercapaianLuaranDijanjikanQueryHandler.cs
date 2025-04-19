using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PotensiKetercapaianLuaranDijanjikan.Domain.PotensiKetercapaianLuaranDijanjikan;

namespace UnpakSipaksi.Modules.PotensiKetercapaianLuaranDijanjikan.Application.GetBobotPotensiKetercapaianLuaranDijanjikan
{
    internal sealed class GetBobotPotensiKetercapaianLuaranDijanjikanQueryHandler(IDbConnectionFactory dbConnectionFactory)
        : IQueryHandler<GetBobotPotensiKetercapaianLuaranDijanjikanQuery, int?>
    {
        public async Task<Result<int?>> Handle(GetBobotPotensiKetercapaianLuaranDijanjikanQuery request, CancellationToken cancellationToken)
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
                return Result.Failure<int?>(PotensiKetercapaianLuaranDijanjikanErrors.UnknownKategoriSkema());
            }

            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            var sql = $"""
                SELECT DISTINCT {column}
                FROM potensi_ketercapaian_luaran_dijanjikan
            """;

            var nilaiList = (await connection.QueryAsync<int>(sql)).ToList();

            if (!nilaiList.Any())
            {
                return Result.Failure<int?>(PotensiKetercapaianLuaranDijanjikanErrors.EmptyData());
            }

            if (nilaiList.Count > 1)
            {
                return Result.Failure<int?>(PotensiKetercapaianLuaranDijanjikanErrors.NotSameValue());
            }

            return Result.Success(nilaiList?.First());
        }
    }
}
