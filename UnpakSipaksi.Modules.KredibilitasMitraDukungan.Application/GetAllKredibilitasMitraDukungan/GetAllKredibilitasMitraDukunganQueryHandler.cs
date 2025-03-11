using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KredibilitasMitraDukungan.Application.GetKredibilitasMitraDukungan;
using UnpakSipaksi.Modules.KredibilitasMitraDukungan.Domain.KredibilitasMitraDukungan;

namespace UnpakSipaksi.Modules.KredibilitasMitraDukungan.Application.GetAllKredibilitasMitraDukungan
{
    internal sealed class GetAllKredibilitasMitraDukunganQueryHandler(IDbConnectionFactory _dbConnectionFactory) : IQueryHandler<GetAllKredibilitasMitraDukunganQuery, List<KredibilitasMitraDukunganResponse>>
    {
        public async Task<Result<List<KredibilitasMitraDukunganResponse>>> Handle(GetAllKredibilitasMitraDukunganQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await _dbConnectionFactory.OpenConnectionAsync();

            const string sql =
            """
            SELECT 
                CAST(NULLIF(uuid, '') AS VARCHAR(36)) AS Uuid,
                name as Nama,
                bobot_pdp AS BobotPDP,
                bobot_terapan AS BobotTerapan,
                bobot_kerjasama AS BobotKerjasama,
                bobot_penelitian_dasar AS BobotPenelitianDasar,
                skor AS Skor 
            FROM kredibilitas_mitra_dukungan
            """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QueryAsync<KredibilitasMitraDukunganResponse>(sql);

            if (result == null || !result.Any())
            {
                return Result.Failure<List<KredibilitasMitraDukunganResponse>>(KredibilitasMitraDukunganErrors.EmptyData());
            }

            return Result.Success(result.ToList());
        }
    }
}
