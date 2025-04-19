using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.JumlahKolaboratorPublikasBereputasi.Domain.JumlahKolaboratorPublikasBereputasi;

namespace UnpakSipaksi.Modules.JumlahKolaboratorPublikasBereputasi.Application.GetJumlahKolaboratorPublikasBereputasi
{
    internal sealed class GetJumlahKolaboratorPublikasBereputasiQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetJumlahKolaboratorPublikasBereputasiQuery, JumlahKolaboratorPublikasBereputasiResponse>
    {
        public async Task<Result<JumlahKolaboratorPublikasBereputasiResponse>> Handle(GetJumlahKolaboratorPublikasBereputasiQuery request, CancellationToken cancellationToken)
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
                 FROM jumlah_kolaborator_publikasi_bereputasi 
                 WHERE uuid = @Uuid
                 """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QuerySingleOrDefaultAsync<JumlahKolaboratorPublikasBereputasiResponse?>(sql, new { Uuid = request.JumlahKolaboratorPublikasBereputasiUuid });
            if (result == null)
            {
                return Result.Failure<JumlahKolaboratorPublikasBereputasiResponse>(JumlahKolaboratorPublikasBereputasiErrors.NotFound(request.JumlahKolaboratorPublikasBereputasiUuid));
            }

            return result;
        }
    }
}
