using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.RelevansiKepakaranTemaProposal.Application.GetRelevansiKepakaranTemaProposal;
using UnpakSipaksi.Modules.RelevansiKepakaranTemaProposal.Domain.RelevansiKepakaranTemaProposal;

namespace UnpakSipaksi.Modules.RelevansiKepakaranTemaProposal.Application.GetAllRelevansiKepakaranTemaProposal
{
    internal sealed class GetAllRelevansiKepakaranTemaProposalQueryHandler(IDbConnectionFactory _dbConnectionFactory) : IQueryHandler<GetAllRelevansiKepakaranTemaProposalQuery, List<RelevansiKepakaranTemaProposalResponse>>
    {
        public async Task<Result<List<RelevansiKepakaranTemaProposalResponse>>> Handle(GetAllRelevansiKepakaranTemaProposalQuery request, CancellationToken cancellationToken)
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
            FROM relevansi_kepakaran_tema_proposal
            """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QueryAsync<RelevansiKepakaranTemaProposalResponse>(sql);

            if (result == null || !result.Any())
            {
                return Result.Failure<List<RelevansiKepakaranTemaProposalResponse>>(RelevansiKepakaranTemaProposalErrors.EmptyData());
            }

            return Result.Success(result.ToList());
        }
    }
}
