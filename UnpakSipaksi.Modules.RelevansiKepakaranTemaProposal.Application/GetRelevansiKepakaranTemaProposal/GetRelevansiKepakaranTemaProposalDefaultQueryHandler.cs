using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.RelevansiKepakaranTemaProposal.Domain.RelevansiKepakaranTemaProposal;

namespace UnpakSipaksi.Modules.RelevansiKepakaranTemaProposal.Application.GetRelevansiKepakaranTemaProposal
{
    internal sealed class GetRelevansiKepakaranTemaProposalDefaultQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetRelevansiKepakaranTemaProposalDefaultQuery, RelevansiKepakaranTemaProposalDefaultResponse>
    {
        public async Task<Result<RelevansiKepakaranTemaProposalDefaultResponse>> Handle(GetRelevansiKepakaranTemaProposalDefaultQuery request, CancellationToken cancellationToken)
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
                 FROM relevansi_kepakaran_tema_proposal 
                 WHERE uuid = @Uuid
                 """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QuerySingleOrDefaultAsync<RelevansiKepakaranTemaProposalDefaultResponse?>(sql, new { Uuid = request.RelevansiKepakaranTemaProposalUuid });
            if (result == null)
            {
                return Result.Failure<RelevansiKepakaranTemaProposalDefaultResponse>(RelevansiKepakaranTemaProposalErrors.NotFound(request.RelevansiKepakaranTemaProposalUuid));
            }

            return result;
        }
    }
}
