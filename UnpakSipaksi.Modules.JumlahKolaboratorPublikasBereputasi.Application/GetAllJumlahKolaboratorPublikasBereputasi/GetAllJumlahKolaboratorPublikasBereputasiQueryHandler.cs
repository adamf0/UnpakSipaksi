using Dapper;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.JumlahKolaboratorPublikasBereputasi.Domain.JumlahKolaboratorPublikasBereputasi;
using UnpakSipaksi.Modules.JumlahKolaboratorPublikasBereputasi.Application.GetJumlahKolaboratorPublikasBereputasi;

namespace UnpakSipaksi.Modules.JumlahKolaboratorPublikasBereputasi.Application.GetAllJumlahKolaboratorPublikasBereputasi
{
    internal sealed class GetAllJumlahKolaboratorPublikasBereputasiQueryHandler(IDbConnectionFactory _dbConnectionFactory) : IQueryHandler<GetAllJumlahKolaboratorPublikasBereputasiQuery, List<JumlahKolaboratorPublikasBereputasiResponse>>
    {
        public async Task<Result<List<JumlahKolaboratorPublikasBereputasiResponse>>> Handle(GetAllJumlahKolaboratorPublikasBereputasiQuery request, CancellationToken cancellationToken)
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
            FROM jumlah_kolaborator_publikasi_bereputasi
            """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QueryAsync<JumlahKolaboratorPublikasBereputasiResponse>(sql);

            if (result == null || !result.Any())
            {
                return Result.Failure<List<JumlahKolaboratorPublikasBereputasiResponse>>(JumlahKolaboratorPublikasBereputasiErrors.EmptyData());
            }

            return Result.Success(result.ToList());
        }
    }
}
