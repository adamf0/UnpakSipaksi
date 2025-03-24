using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PublikasiDisitasiProposal.Domain.PublikasiDisitasiProposal;

namespace UnpakSipaksi.Modules.PublikasiDisitasiProposal.Application.GetPublikasiDisitasiProposal
{
    internal sealed class GetPublikasiDisitasiProposalQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetPublikasiDisitasiProposalQuery, PublikasiDisitasiProposalResponse>
    {
        public async Task<Result<PublikasiDisitasiProposalResponse>> Handle(GetPublikasiDisitasiProposalQuery request, CancellationToken cancellationToken)
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
                 FROM publikasi_disitasi_proposal 
                 WHERE uuid = @Uuid
                 """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QuerySingleOrDefaultAsync<PublikasiDisitasiProposalResponse?>(sql, new { Uuid = request.PublikasiDisitasiProposalUuid });
            if (result == null)
            {
                return Result.Failure<PublikasiDisitasiProposalResponse>(PublikasiDisitasiProposalErrors.NotFound(request.PublikasiDisitasiProposalUuid));
            }

            return result;
        }
    }
}
