using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PublikasiDisitasiProposal.Domain.PublikasiDisitasiProposal;

namespace UnpakSipaksi.Modules.PublikasiDisitasiProposal.Application.GetPublikasiDisitasiProposal
{
    internal sealed class GetPublikasiDisitasiProposalDefaultQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetPublikasiDisitasiProposalDefaultQuery, PublikasiDisitasiProposalDefaultResponse>
    {
        public async Task<Result<PublikasiDisitasiProposalDefaultResponse>> Handle(GetPublikasiDisitasiProposalDefaultQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            //CAST(NULLIF(id_group, '') AS CHAR(36)) -> guid
            const string sql =
                $"""
                 SELECT 
                     id as Id,
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

            var result = await connection.QuerySingleOrDefaultAsync<PublikasiDisitasiProposalDefaultResponse?>(sql, new { Uuid = request.PublikasiDisitasiProposalUuid });
            if (result == null)
            {
                return Result.Failure<PublikasiDisitasiProposalDefaultResponse>(PublikasiDisitasiProposalErrors.NotFound(request.PublikasiDisitasiProposalUuid));
            }

            return result;
        }
    }
}
