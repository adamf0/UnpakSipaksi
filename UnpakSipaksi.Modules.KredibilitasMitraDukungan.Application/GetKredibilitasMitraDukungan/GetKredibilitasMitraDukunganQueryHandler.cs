using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KredibilitasMitraDukungan.Domain.KredibilitasMitraDukungan;

namespace UnpakSipaksi.Modules.KredibilitasMitraDukungan.Application.GetKredibilitasMitraDukungan
{
    internal sealed class GetKredibilitasMitraDukunganQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetKredibilitasMitraDukunganQuery, KredibilitasMitraDukunganResponse>
    {
        public async Task<Result<KredibilitasMitraDukunganResponse>> Handle(GetKredibilitasMitraDukunganQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            //CAST(NULLIF(id_group, '') AS CHAR(36)) -> guid
            const string sql =
                $"""
                 SELECT 
                     CAST(NULLIF(uuid, '') AS VARCHAR(36)) AS Uuid,
                     name as Nama,
                     bobot_pdp AS BobotPDP,
                     bobot_terapan AS BobotTerapan,
                     bobot_kerjasama AS BobotKerjasama,
                     bobot_penelitian_dasar AS BobotPenelitianDasar,
                     skor AS Skor 
                 FROM kredibilitas_mitra_dukungan 
                 WHERE uuid = @Uuid
                 """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QuerySingleOrDefaultAsync<KredibilitasMitraDukunganResponse?>(sql, new { Uuid = request.KredibilitasMitraDukunganUuid });
            if (result == null)
            {
                return Result.Failure<KredibilitasMitraDukunganResponse>(KredibilitasMitraDukunganErrors.NotFound(request.KredibilitasMitraDukunganUuid));
            }

            return result;
        }
    }
}
