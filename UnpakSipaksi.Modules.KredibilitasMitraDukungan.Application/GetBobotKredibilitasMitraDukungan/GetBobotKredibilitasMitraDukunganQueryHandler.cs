using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KredibilitasMitraDukungan.Domain.KredibilitasMitraDukungan;

namespace UnpakSipaksi.Modules.KredibilitasMitraDukungan.Application.GetBobotKredibilitasMitraDukungan
{
    internal sealed class GetBobotKredibilitasMitraDukunganQueryHandler(IDbConnectionFactory dbConnectionFactory)
        : IQueryHandler<GetBobotKredibilitasMitraDukunganQuery, int?>
    {
        public async Task<Result<int?>> Handle(GetBobotKredibilitasMitraDukunganQuery request, CancellationToken cancellationToken)
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
                return Result.Failure<int?>(KredibilitasMitraDukunganErrors.UnknownKategoriSkema());
            }

            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            var sql = $"""
                SELECT DISTINCT {column}
                FROM kredibilitas_mitra_dukungan
            """;

            var nilaiList = (await connection.QueryAsync<int>(sql)).ToList();

            if (!nilaiList.Any())
            {
                return Result.Failure<int?>(KredibilitasMitraDukunganErrors.EmptyData());
            }

            if (nilaiList.Count > 1)
            {
                return Result.Failure<int?>(KredibilitasMitraDukunganErrors.NotSameValue());
            }

            return Result.Success(nilaiList?.First());
        }
    }
}
